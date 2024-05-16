using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Accuracy;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Actions.BattleActions.Interfaces;
using CharacterModule.CharacterType.Base;

namespace BattleModule.Actions.BattleActions.Base
{
    public abstract class BattleAction
    {
        protected BattleActionContext BattleActionContext;

        protected IAction ActionObject;

        public Action OnActionFinished = delegate { };

        public BattleActionContext GetBattleActionContext() 
        {
            return BattleActionContext;
        }

        public void Init(object actionObject)
        {
            BattleActionContext = new BattleActionContext(actionObject);

            ActionObject = (actionObject as IActionProvider)?.GetAction();
        }

        public virtual void PerformAction(Character source, List<Character> targets,
            Dictionary<Character, BattleAccuracy> accuracies)
        {
            source.WeaponController.GetSpecialAttack().Charge(5f);
            
            ActionObject.ApplyModifiers(
                source.CharacterStats.StatModifierManager, 
                targets.Select(c => c.CharacterStats.StatModifierManager).ToList());
            
            OnActionFinished?.Invoke();
        }
    }
}
