using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Transformer;
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

        public List<BattleActionOutcome> EvaluateAccuracies(List<Character> targets)
        {
            var accuracyResult = new List<BattleActionOutcome>();

            var accuracies = _battleAccuracyController.GetAccuracies();
            
            foreach (var target in targets)
            {
                var initialOutcome = accuracies[target].Evaluate();

                initialOutcome = _outcomeTransformers[target].Aggregate(initialOutcome, (current, outcomeTransformer) => outcomeTransformer.TransformOutcome(current));

                accuracyResult.Add(initialOutcome);
            }
            
            return accuracyResult;
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
                
                _outcomeTransformers[transformer.Key].AddRange(cloned);
            }
        }

        public UniTask Load(List<Character> characters)
        {
            ProcessPassiveTransformers(characters);
            
            return UniTask.CompletedTask;
        }
    }
}