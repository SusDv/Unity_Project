using BattleModule.ActionCore.Context;
using BattleModule.Utility;
using StatModule.Utility.Enums;

namespace BattleModule.ActionCore
{
    public class BattleDefaultAction : BattleAction 
    {
        private BattleDefaultAction(BattleActionContext battleActionContext) 
            : base(battleActionContext) {}

        public override void PerformAction(Character source, Character target)
        {
            float damage = -BattleAttackDamageProcessor.CalculateAttackDamage(
                    source.GetStats().GetStatFinalValue(StatType.ATTACK),
                    target.GetStats().GetStatFinalValue(StatType.DEFENSE));

            target.GetStats().ApplyStatModifier(StatType.HEALTH,
                damage);
            
            source.GetStats().ApplyStatModifier(StatType.BATTLE_POINTS, 10);
        }

        public static BattleDefaultAction GetBattleDefaultActionInstance(
            BattleActionContext battleActionContext) 
        {
            return new BattleDefaultAction(battleActionContext);
        }
    }
}
