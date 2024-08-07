using System;
using System.Collections.Generic;
using BattleModule.Actions.Context;
using BattleModule.Actions.Interfaces;
using BattleModule.Actions.Outcome;
using BattleModule.Controllers.Modules;
using BattleModule.Utility;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Animation;
using CharacterModule.Types.Base;
using CharacterModule.Utility;
using CharacterModule.Utility.Stats;
using Cysharp.Threading.Tasks;

namespace BattleModule.Actions.Base
{
    public abstract class BattleAction
    {
        private BattleActionContext _battleActionContext;

        private IAction _actionObject;

        private BattleDamage _battleDamage;

        private BattleActionController.ActionData _actionData;

        protected abstract string ActionAnimationName { get; }

        protected abstract ActionType ActionType { get; }

        public BattleActionContext Init(object actionObject, 
            BattleActionController.ActionData actionData)
        {
            _actionData = actionData;
            
            _battleActionContext = new BattleActionContext(actionObject, _actionData.CharacterType, ActionType);

            _actionObject = (actionObject as IActionProvider)?.GetAction();

            _battleDamage = GetDamageCalculator(_actionData.StatsController.GetStatsInfo());

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

        private void EndAction()
        {
            _actionData.SpecialAttack.Charge(5f);
            
            _actionData.StatsController.ApplyInstantModifier(StatType.BATTLE_POINTS, _battleActionContext.BattleObject.BattlePoints);
        }

        public async UniTask<List<BattleActionOutcome>> PerformAction(
            List<Character> targets,
            BattleOutcomeController battleOutcomeController)
        {
            var attackResults = battleOutcomeController.CalculateActiveTransformers(targets);

            var animationResult = await PlayActionAnimation(_actionData.AnimationManager, targets, () =>
            {
                AttackTargets(targets, attackResults, battleOutcomeController);

                EndAction();
            });
            
            if (!animationResult)
            {
                throw new Exception();
            }

            return attackResults;
        }
        
        protected virtual async UniTask<bool> PlayActionAnimation(AnimationManager source, 
            IEnumerable<Character> targets, Action triggerCallback)
        {
            return await source.PlayAnimation(ActionAnimationName, triggerCallback);
        }

        protected virtual BattleDamage GetDamageCalculator(Dictionary<StatType, StatInfo> statsController)
        {
            return new PhysicalDamage(statsController);
        }
    }
}
