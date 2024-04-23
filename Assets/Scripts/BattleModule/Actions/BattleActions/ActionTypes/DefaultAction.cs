using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.AccuracyModule;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.Utility.Enums;

namespace BattleModule.Actions.BattleActions.ActionTypes
{
    public class DefaultAction : BattleAction
    {
        private DefaultAction()
        {
            Accuracy = new DamageAccuracy();
        }

        public override void PerformAction(Character source, List<Character> targets, Action actionFinishedCallback)
        {
            var characterStats = source.CharacterStats;
                
            var target = targets.First().CharacterStats;

            target.ApplyStatModifier(StatType.HEALTH, -characterStats.GetStatInfo(StatType.ATTACK).FinalValue);

            base.PerformAction(source, targets, actionFinishedCallback);
        }
    }
}
