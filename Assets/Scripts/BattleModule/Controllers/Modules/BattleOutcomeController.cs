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

        private void ProcessEquipmentTransformers(List<Character> characters)
        {
            _outcomeTransformers.Clear();

            foreach (var character in characters)
            {
                AddDistinctOutcomes(character, character.EquipmentController.GetPassiveTransformers());
            }
        }

        private static List<OutcomeTransformer> CloneTransformers(
            IEnumerable<OutcomeTransformer> outcomeTransformers)
        {
            return outcomeTransformers.Select(transformer => transformer.Clone()).ToList();
        }
        
        private static BattleActionOutcome GetOutcomeByType(SubIntervalType subIntervalType)
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

        private void SetOutcomeTimers(IEnumerable<OutcomeTransformer> outcomeTransformers)
        {
            outcomeTransformers.Where(t => !t.Initialized).ToList()
                .ForEach(t => t.SetTimerFactory(_battleTimerController.CreateTimer));
        }
        
        private void AddDistinctOutcomes(Character character, IEnumerable<OutcomeTransformer> outcomeTransformers)
        {
            var clonedTransformers = CloneTransformers(outcomeTransformers);
            
            SetOutcomeTimers(clonedTransformers);

            if (!_outcomeTransformers.TryGetValue(character, out _))
            {
                _outcomeTransformers.Add(character, clonedTransformers.Distinct().ToList());
                
                return;
            }

            foreach (var transformer in clonedTransformers.Where(outcomeTransformer =>
                         !_outcomeTransformers[character].Contains(outcomeTransformer)))
            {
                _outcomeTransformers[character].Add(transformer);
            }
        }

        public List<BattleActionOutcome> CalculateStaticTransformers(
            IEnumerable<Character> targets)
        {
            var accuracyResult = new List<BattleActionOutcome>();
            
            var accuracies = _battleAccuracyController.GetAccuracies();

            foreach (var target in targets)
            {
                var outcomeToTransform = accuracies[target].Evaluate();

                foreach (var transformer in _outcomeTransformers[target].Where(t => t.IsApplicable(outcomeToTransform.SubIntervalType)))
                {
                    outcomeToTransform = GetOutcomeByType(transformer.GetTransformedType(outcomeToTransform));
                }
                
                accuracyResult.Add(outcomeToTransform);
            }

            return accuracyResult;
        }
        

        public BattleActionOutcome ProcessHitTransformers(Character target,
            BattleActionOutcome battleActionOutcome, 
            List<OutcomeTransformer> outcomeTransformers)
        {
            var outcomeToTransform = battleActionOutcome;
            
            SetOutcomeTimers(outcomeTransformers);
            
            outcomeTransformers.OfType<StaticOutcomeTransformer>()
                .Where(t => t.IsApplicable(battleActionOutcome.SubIntervalType))
                .ToList()
                .ForEach(o =>
            {
                outcomeToTransform = GetOutcomeByType(o.GetTransformedType(outcomeToTransform));
            });
            
            AddDistinctOutcomes(target, outcomeTransformers.OfType<TemporaryOutcomeTransformer>().Cast<OutcomeTransformer>().ToList());
            
            return outcomeToTransform;
        }
        
        public UniTask Load(List<Character> characters)
        {
            ProcessEquipmentTransformers(characters);
            
            return UniTask.CompletedTask;
        }
    }
}