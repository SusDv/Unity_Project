using System;
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

        public Action OnActionFinished = delegate { };

        public BattleActionContext GetBattleActionContext() 
        {
            return _battleActionContext;
        }

        public void Init(object actionObject)
        {
            _battleActionContext = new BattleActionContext(actionObject);

            ActionObject = (actionObject as IActionProvider)?.GetAction();
        }

        public virtual void PerformAction(Character source, 
            List<Character> targets,
            Dictionary<Character, BattleAccuracy> accuracies)
        {
            source.EquipmentController.WeaponController.GetSpecialAttack().Charge(5f);
            
            OnActionFinished?.Invoke();
        }
    }
}
