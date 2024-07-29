using System;
using System.Collections.Generic;
using BattleModule.Actions.Context;
using BattleModule.Actions.Interfaces;
using BattleModule.Actions.Outcome;
using BattleModule.Controllers.Modules;
using BattleModule.Utility;
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

        protected abstract ActionType ActionType { get; }

        public BattleActionContext Init(object actionObject, 
            StatsController statsController)
        {
            _battleActionContext = new BattleActionContext(actionObject, ActionType);

            _actionObject = (actionObject as IActionProvider)?.GetAction();

            _battleDamage = GetDamageCalculator(statsController);

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

            bool animationStatus = await PlayActionAnimation(source, targets, () =>
            {
                AttackTargets(targets, attackResults, battleOutcomeController);
            
                EndAction(source);
            });
            
            return (animationStatus, attackResults);
        }
        
        protected virtual async UniTask<bool> PlayActionAnimation(Character source, 
            IEnumerable<Character> targets, Action triggerCallback)
        {
            return await source.AnimationManager.PlayAnimation(ActionAnimationName, 0.5f, triggerCallback);
        }

        protected virtual BattleDamage GetDamageCalculator(StatsController statsController)
        {
            return new PhysicalDamage(statsController);
        }
    }
}
