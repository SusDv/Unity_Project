using System.Collections.Generic;
using System.Linq;
using BattleModule.Accuracy;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule.Stats.Utility.Enums;
using CharacterModule.Types.Base;

namespace BattleModule.Actions.BattleActions.Types
{
    public class DefaultAction : BattleAction
    {
        public override void PerformAction(Character source,
            List<Character> targets, Dictionary<Character, BattleAccuracy> accuracies)
        {
            var characterStats = source.CharacterStats;
                
            var target = targets.First().CharacterStats;
            
            float damage = -characterStats.GetStatInfo(StatType.ATTACK).FinalValue;
            
            target.ApplyInstantModifier(StatType.HEALTH, damage);

            base.PerformAction(source, targets, accuracies);
        }
    }
}
