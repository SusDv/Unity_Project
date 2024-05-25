using System.Collections.Generic;
using System.Linq;
using BattleModule.Accuracy;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule.Types.Base;
using CharacterModule.Utility;

namespace BattleModule.Actions.BattleActions.Types
{
    public class DefaultAction : BattleAction
    {
        public override void PerformAction(Character source,
            List<Character> targets, Dictionary<Character, BattleAccuracy> accuracies)
        {
            var characterStats = source.Stats;
                
            var target = targets.First().Stats;
            
            float damage = -characterStats.GetStatInfo(StatType.ATTACK).FinalValue;
            
            target.ApplyInstantModifier(StatType.HEALTH, damage);

            base.PerformAction(source, targets, accuracies);
        }
    }
}
