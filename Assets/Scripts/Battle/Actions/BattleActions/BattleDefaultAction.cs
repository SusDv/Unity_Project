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
                    source.GetCharacterStats().GetStatFinalValue(StatType.ATTACK),
                    target.GetCharacterStats().GetStatFinalValue(StatType.DEFENSE));

            target.GetCharacterStats().ApplyStatModifier(StatType.HEALTH,
                damage);
            
            source.GetCharacterStats().ApplyStatModifier(StatType.BATTLE_POINTS, source.GetCharacterWeapon().GetWeaponAttackCost());
        }

        public static BattleDefaultAction GetBattleDefaultActionInstance(
            BattleActionContext battleActionContext) 
        {
            return new BattleDefaultAction(battleActionContext);
        }
    }
}
