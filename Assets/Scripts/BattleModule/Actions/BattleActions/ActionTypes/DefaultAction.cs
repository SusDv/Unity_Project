using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.AccuracyModule;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Actions.BattleActions.Outcome;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.Utility.Enums;
using Random = UnityEngine.Random;

namespace BattleModule.Actions.BattleActions.ActionTypes
{
    public class DefaultAction : BattleAction
    {
        public override void PerformAction(Character source, List<Character> targets, Action actionFinishedCallback)
        {
            var characterStats = source.CharacterStats;
                
            var target = targets.First().CharacterStats;
            
            Accuracy.CalculateIntervalRange(characterStats.GetStatInfo(StatType.ACCURACY).FinalValue, target.GetStatInfo(StatType.EVASION).FinalValue);
            
            target.StatModifierManager.ApplyInstantModifier(StatType.HEALTH, -characterStats.GetStatInfo(StatType.ATTACK).FinalValue);

            base.PerformAction(source, targets, actionFinishedCallback);
        }
    }
}
