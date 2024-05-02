using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.Utility.Enums;

namespace BattleModule.Actions.BattleActions.ActionTypes
{
    public class DefaultAction : BattleAction
    {
        public override void PerformAction(Character source, List<Character> targets, Action actionFinishedCallback)
        {
            var characterStats = source.CharacterStats;
                
            var target = targets.First().CharacterStats;
            
            Accuracy.CalculateIntervalRange(characterStats.GetStatInfo(StatType.ACCURACY).FinalValue, target.GetStatInfo(StatType.EVASION).FinalValue);
            
            float damage = -characterStats.GetStatInfo(StatType.ATTACK).FinalValue;
            
            target.StatModifierManager.ApplyInstantModifier(StatType.HEALTH, damage);

            base.PerformAction(source, targets, actionFinishedCallback);
        }
    }
}
