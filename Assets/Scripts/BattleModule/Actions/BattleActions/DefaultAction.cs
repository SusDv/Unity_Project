using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Utility;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.Utility.Enums;

namespace BattleModule.Actions.BattleActions
{
    public class DefaultAction : BattleAction
    {
        public override void PerformAction(Character source, List<Character> targets, Action actionFinishedCallback)
        {
            source.AnimationManager.PlayAnimation("Attack", () =>
            {
                var characterStats = source.CharacterStats;
                
                var target = targets.First().CharacterStats;

                float damage = -BattleActionAccuracy
                    .CalculateAttackDamage(
                        characterStats.GetStatInfo(StatType.ATTACK),
                        characterStats.GetStatInfo(StatType.CRITICAL_DAMAGE),
                        target.GetStatInfo(StatType.DEFENSE));

                target.ApplyStatModifier(StatType.HEALTH, damage);

                base.PerformAction(source, targets, actionFinishedCallback);
            });
        }
    }
}
