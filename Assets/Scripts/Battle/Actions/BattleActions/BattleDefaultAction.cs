using BattleModule.ActionCore.Context;
using BattleModule.Utility;
using InventorySystem.Item;
using StatModule.Interfaces;
using StatModule.Utility.Enums;
using System.Collections.Generic;

namespace BattleModule.ActionCore
{
    public class BattleDefaultAction : BattleAction 
    {
        private BattleDefaultAction(BattleActionContext battleActionContext) 
            : base(battleActionContext) 
        {}

        public override void PerformAction(IHaveStats source, List<Character> targets)
        {
            WeaponItem characterWeapon = _battleActionContext.ActionObject as WeaponItem;

            foreach(Character character in targets) 
            {
                IHaveStats target = character.GetCharacterStats();

                float damage = -BattleAttackDamageProcessor.CalculateAttackDamage(
                    source.GetStatFinalValue(StatType.ATTACK),
                    target.GetStatFinalValue(StatType.DEFENSE));

                target.AddStatModifier(StatType.HEALTH,
                    damage);
            }

            source.AddStatModifier(StatType.BATTLE_POINTS, characterWeapon.BattlePoints);
        }

        public static BattleDefaultAction GetBattleDefaultActionInstance(
            BattleActionContext battleActionContext) 
        {
            return new BattleDefaultAction(battleActionContext);
        }
    }
}
