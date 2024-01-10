using BattleModule.Utility;
using BattleModule.Utility.Enums;
using StatModule.Utility.Enums;

namespace BattleModule.ActionCore
{
    public class BattleDefaultAction : BattleAction 
    {
        private BattleDefaultAction(TargetType targetType) 
            : base(null, targetType) {}

        public override void PerformAction(Character source, Character target)
        {
            float damage = -BattleAttackDamageProcessor.CalculateAttackDamage(
                    source.GetStats().GetStatFinalValue(StatType.ATTACK),
                    target.GetStats().GetStatFinalValue(StatType.DEFENSE));

            target.GetStats().ModifyStat(StatType.HEALTH,
                damage);
            
            source.GetStats().ModifyStat(StatType.BATTLE_POINTS, 10);
        }

        public static BattleDefaultAction GetBattleDefaultActionInstance(TargetType targetType = TargetType.ENEMY) 
        {
            return new BattleDefaultAction(targetType);
        }
    }
}
