using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Actions.BattleActions.Transformer;
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
                SetOutcomeTimers(character.EquipmentController.GetPassiveTransformers());
                
                _outcomeTransformers.Add(character, character.EquipmentController.GetPassiveTransformers());
            }
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

        public void SetOutcomeTimers(List<OutcomeTransformer> outcomeTransformers)
        {
            outcomeTransformers.ForEach(t => t.SetTimer(_battleTimerController.CreateTimer()));
        }

        public void AddTransformer(Dictionary<Character, List<OutcomeTransformer>> toAdd)
        {
            foreach (var transformer in toAdd)
            {
                SetOutcomeTimers(transformer.Value);
                
                _outcomeTransformers[transformer.Key].AddRange(transformer.Value);
            }
        }

        public Dictionary<Character, List<OutcomeTransformer>> GetTransformers()
        {
            return _outcomeTransformers;
        }

        public UniTask Load(List<Character> characters)
        {
            ProcessPassiveTransformers(characters);
            
            return UniTask.CompletedTask;
        }
    }
}