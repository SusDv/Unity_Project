using System;
using System.Collections.Generic;
using BattleModule.Accuracy;
using BattleModule.Actions.BattleActions.Context;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.Utility.Enums;

namespace BattleModule.Actions.BattleActions.Base
{
    public abstract class BattleAction
    {
        protected BattleActionContext BattleActionContext;

        public BattleActionContext GetBattleActionContext() 
        {
            return BattleActionContext;
        }

        public void Init(object actionObject)
        {
            BattleActionContext = new BattleActionContext(actionObject);
        }

        public virtual void PerformAction(Character source,
            List<Character> targets,
            Action actionFinishedCallback)
        {
            source.WeaponController.GetSpecialAttack().Charge(5f);
            
            source.CharacterStats.StatModifierManager.ApplyInstantModifier(StatType.BATTLE_POINTS, BattleActionContext.BattleObject.BattlePoints);
            
            actionFinishedCallback?.Invoke();
        }
    }
}
