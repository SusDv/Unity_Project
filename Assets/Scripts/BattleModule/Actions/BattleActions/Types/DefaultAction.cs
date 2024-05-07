using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Accuracy;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.Utility.Enums;

namespace BattleModule.Actions.BattleActions.Types
{
    public class DefaultAction : BattleAction
    {
        public override void PerformAction(Character source,
            List<Character> targets,
            Action actionFinishedCallback)
        {
            var characterStats = source.CharacterStats;
                
            var target = targets.First().CharacterStats;
            
            float damage = -characterStats.GetStatInfo(StatType.ATTACK).FinalValue;
            
            target.StatModifierManager.ApplyInstantModifier(StatType.HEALTH, damage);

            base.PerformAction(source, targets, actionFinishedCallback);
        }
    }
}
