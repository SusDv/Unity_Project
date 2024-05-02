using System;
using System.Collections.Generic;
using BattleModule.AccuracyModule;
using BattleModule.AccuracyModule.AccuracyRange;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.Utility.Enums;

namespace BattleModule.Actions.BattleActions.Base
{
    public abstract class BattleAction
    {
        protected BattleActionContext BattleActionContext;

        protected Accuracy Accuracy;

        public BattleActionContext GetBattleActionContext() 
        {
            return BattleActionContext;
        }

        public void Init(object actionObject)
        {
            BattleActionContext = new BattleActionContext(actionObject);

            Accuracy = new DamageAccuracy();
        }

        public virtual void PerformAction(Character source, List<Character> targets, Action actionFinishedCallback)
        {
            source.WeaponController.GetSpecialAttack().Charge(5f);
            
            source.CharacterStats.StatModifierManager.ApplyInstantModifier(StatType.BATTLE_POINTS, BattleActionContext.BattleObject.BattlePoints);
            
            actionFinishedCallback?.Invoke();
        }
    }
}
