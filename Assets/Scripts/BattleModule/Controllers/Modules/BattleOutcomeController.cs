using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Outcome.Outcomes;
using BattleModule.Actions.Transformer;
using BattleModule.Utility;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using Utility;

namespace BattleModule.Controllers.Modules
{
    public class BattleOutcomeController : ILoadingUnit<List<Character>>
    {
        private readonly BattleTimerController _battleTimerController;

        private readonly BattleAccuracyController _battleAccuracyController;
        
        private readonly Dictionary<Character, List<OutcomeTransformer>> _outcomeTransformers = new();
        
        private BattleOutcomeController(BattleTimerController battleTimerController,
            BattleAccuracyController battleAccuracyController)
        {
            _battleTimerController = battleTimerController;

            _battleAccuracyController = battleAccuracyController;
        }

        private void ProcessPassiveTransformers(List<Character> characters)
        {
            _outcomeTransformers.Clear();

            foreach (var character in characters)
            {
                var cloned = CloneTransformers(character.EquipmentController.GetPassiveTransformers());
                
                SetOutcomeTimers(cloned);
                
                _outcomeTransformers.Add(character, cloned);
            }
        }

        private List<OutcomeTransformer> CloneTransformers(IEnumerable<OutcomeTransformer> outcomeTransformers)
        {
            return outcomeTransformers.Select(transformer => transformer.Clone()).ToList();
        }

        public List<BattleActionOutcome> ProcessAccuracyResult(IEnumerable<Character> targets)
        {
            var accuracyResult = new List<BattleActionOutcome>();
            
            var accuracies = _battleAccuracyController.GetAccuracies();

            foreach (var target in targets)
            {
                var initialOutcome = accuracies[target].Evaluate();

                foreach (var transformer in _outcomeTransformers[target].Where(t => t.IsApplicable(initialOutcome.SubIntervalType)))
                {
                    initialOutcome = GetTransformedOutcome(transformer.GetTransformTo(initialOutcome));
                }
                
                accuracyResult.Add(initialOutcome);
            }

            return accuracyResult;
        }

        public (List<OutcomeTransformer> toAdd, BattleActionOutcome result) ProcessHitTransformers(BattleActionOutcome battleActionOutcome, 
            List<OutcomeTransformer> outcomeTransformers)
        {
            var initialOutcome = battleActionOutcome;
            
            SetOutcomeTimers(outcomeTransformers);
            
            outcomeTransformers.OfType<StaticOutcomeTransformer>().ToList().Where(t => t.IsApplicable(battleActionOutcome.SubIntervalType)).ToList().ForEach(o =>
            {
                initialOutcome = GetTransformedOutcome(o.GetTransformTo(initialOutcome));
            });
            
            return (outcomeTransformers.OfType<TemporaryOutcomeTransformer>().Cast<OutcomeTransformer>().ToList(), initialOutcome);
        }

        public BattleActionOutcome GetTransformedOutcome(SubIntervalType subIntervalType)
        {
            BattleActionOutcome battleActionOutcome = 
                subIntervalType switch
                {
                    SubIntervalType.CRIT => new CritDamage(),
                    SubIntervalType.FULL => new FullDamage(),
                    SubIntervalType.HALF => new HalfDamage(),
                    _ => new TrueMiss()
                };

            return battleActionOutcome;
        }

        public void SetOutcomeTimers(IEnumerable<OutcomeTransformer> outcomeTransformers)
        {
            outcomeTransformers.Where(t => !t.Initialized).ToList()
                .ForEach(t => t.SetTimerFactory(_battleTimerController.CreateTimer));
        }

        public void AddTransformer(Dictionary<Character, List<OutcomeTransformer>> toAdd)
        {
            foreach (var transformer in toAdd)
            {
                var cloned = CloneTransformers(transformer.Value);

                SetOutcomeTimers(cloned);

                foreach (var outcomeTransformer in cloned.Where(outcomeTransformer => !_outcomeTransformers[transformer.Key].Contains(outcomeTransformer)))
                {
                    _outcomeTransformers[transformer.Key].Add(outcomeTransformer);
                }
            }
        }

        public UniTask Load(List<Character> characters)
        {
            ProcessPassiveTransformers(characters);
            
            return UniTask.CompletedTask;
        }
    }
}