using System.Collections.Generic;
using BattleModule.Actions.Context;
using BattleModule.Actions.Interfaces;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Transformer;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Managers;
using CharacterModule.Types.Base;
using CharacterModule.Utility;
using Cysharp.Threading.Tasks;

namespace BattleModule.Actions.Base
{
    public abstract class BattleAction
    {
        private BattleActionContext _battleActionContext;

        private IAction _actionObject;

        private BattleDamage _battleDamage;

        protected abstract string ActionAnimationName { get; }

        public BattleActionContext Init(object actionObject, 
            Character character)
        {
            _battleActionContext = new BattleActionContext(actionObject, character.GetType());

            _actionObject = (actionObject as IActionProvider)?.GetAction();

            _battleDamage = GetDamageCalculator(character.Stats);

            return _battleActionContext;
        }
        
        private Dictionary<Character, List<OutcomeTransformer>> ApplyHitModifiers(
            Character source, 
            IReadOnlyList<Character> targets,
            IList<BattleActionOutcome> accuracyResult,
            BattleOutcomeController battleOutcomeController)
        {
            var result = new Dictionary<Character, List<OutcomeTransformer>>();
            
            for (var i = 0; i < accuracyResult.Count; i++)
            {
                var operation = _actionObject
                    .ProcessAction(targets[i].Stats, 
                    accuracyResult[i], _battleDamage, battleOutcomeController);

                accuracyResult[i] = operation.result;
                
                result.Add(targets[i], operation.toAdd);
            }
            
            source.EquipmentController.WeaponController.GetSpecialAttack().Charge(5f);
            
            source.Stats.ApplyInstantModifier(StatType.BATTLE_POINTS, _battleActionContext.BattleObject.BattlePoints);
            
            return result;
        }
        
        protected virtual async UniTask PlayActionAnimation(Character source, List<Character> targets)
        {
            await source.AnimationManager.PlayAnimation(ActionAnimationName);
        }

        public async UniTask<(Dictionary<Character, List<OutcomeTransformer>> toAdd, List<BattleActionOutcome> result)> PerformAction(
            Character source,
            List<Character> targets,
            BattleOutcomeController battleOutcomeController)
        {
            var accuracyResult = battleOutcomeController.EvaluateAccuracies(targets);
            
            await PlayActionAnimation(source, targets);
            
            return (ApplyHitModifiers(source, targets, accuracyResult, battleOutcomeController), accuracyResult);
        }

        protected virtual BattleDamage GetDamageCalculator(StatManager statManager)
        {
            return new PhysicalDamage(statManager);
        }
    }
}
