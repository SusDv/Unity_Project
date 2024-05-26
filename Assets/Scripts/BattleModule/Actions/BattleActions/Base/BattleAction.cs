using System.Collections.Generic;
using BattleModule.Accuracy;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Actions.BattleActions.Interfaces;
using CharacterModule.Types.Base;

namespace BattleModule.Actions.BattleActions.Base
{
    public abstract class BattleAction
    {
        private BattleActionContext _battleActionContext;

        protected IAction ActionObject;

        public BattleActionContext Init(object actionObject, Character currentCharacterType)
        {
            _battleActionContext = new BattleActionContext(actionObject, currentCharacterType);

            ActionObject = (actionObject as IActionProvider)?.GetAction();

            return _battleActionContext;
        }

        public virtual void PerformAction(Character source, 
            List<Character> targets,
            Dictionary<Character, BattleAccuracy> accuracies)
        {
            var sourceStats = source.Stats;

            foreach (var target in targets)
            {
                ActionObject.ApplyModifiers(sourceStats, target.Stats, accuracies[target].Evaluate());
            }
            
            source.EquipmentController.WeaponController.GetSpecialAttack().Charge(5f);
        }
    }
}
