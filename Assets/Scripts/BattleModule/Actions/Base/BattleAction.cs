using System.Collections.Generic;
using BattleModule.Actions.Context;
using BattleModule.Actions.Interfaces;
using BattleModule.Actions.Outcome;
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
        
        private void AttackTargets(
            IReadOnlyList<Character> targets,
            IList<BattleActionOutcome> accuracyResult,
            BattleOutcomeController battleOutcomeController)
        {
            for (var i = 0; i < accuracyResult.Count; i++)
            {
                var operation = _actionObject
                    .ProcessAction(targets[i], 
                    accuracyResult[i], _battleDamage, 
                    battleOutcomeController);

                accuracyResult[i] = operation;
            }
        }

        private void EndAction(Character source)
        {
            source.EquipmentController.WeaponController.GetSpecialAttack().Charge(5f);
            
            source.Stats.ApplyInstantModifier(StatType.BATTLE_POINTS, _battleActionContext.BattleObject.BattlePoints);
        }

        public async UniTask<(bool status, List<BattleActionOutcome> result)> PerformAction(
            Character source,
            List<Character> targets,
            BattleOutcomeController battleOutcomeController)
        {
            var attackResults = battleOutcomeController.CalculateActiveTransformers(targets);

            bool animationStatus = await PlayActionAnimation(source, targets);

            if (animationStatus)
            {
                AttackTargets(targets, attackResults, battleOutcomeController);
            
                EndAction(source);
            }
            
            return (animationStatus, attackResults);
        }
        
        protected virtual async UniTask<bool> PlayActionAnimation(Character source, IEnumerable<Character> targets)
        {
            return await source.AnimationManager.PlayAnimation(ActionAnimationName);
        }

        protected virtual BattleDamage GetDamageCalculator(StatsController statsController)
        {
            return new PhysicalDamage(statsController);
        }
    }
}
