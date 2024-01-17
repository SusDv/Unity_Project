using BattleModule.ActionCore.Context;
using BattleModule.Utility;
using StatModule.Utility.Enums;
using System.Collections.Generic;

namespace BattleModule.ActionCore
{
    public class BattleDefaultAction : BattleAction 
    {
        private BattleDefaultAction(BattleActionContext battleActionContext) 
            : base(battleActionContext) 
        {}

        public override void PerformAction(Character source, List<Character> targets)
        {
            foreach(Character target in targets) 
            {
                float damage = -BattleAttackDamageProcessor.CalculateAttackDamage(
                    source.GetCharacterStats().GetStatFinalValue(StatType.ATTACK),
                    target.GetCharacterStats().GetStatFinalValue(StatType.DEFENSE));

                target.GetCharacterStats().AddStatModifier(StatType.HEALTH,
                    damage);
            }

            source.GetCharacterStats().AddStatModifier(StatType.BATTLE_POINTS, source.GetCharacterWeapon().GetWeapon().BattlePoints);
        }

        public static BattleDefaultAction GetBattleDefaultActionInstance(
            BattleActionContext battleActionContext) 
        {
            return new BattleDefaultAction(battleActionContext);
        }
    }
}
