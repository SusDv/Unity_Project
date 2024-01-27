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
        public override string ActionName => "Weapon attack";

        public BattleDefaultAction() 
            : base()
        {}

        private BattleDefaultAction(object actionObject) 
            : base(actionObject) 
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

        public override BattleAction GetInstance(object actionObject)
        {
            return new BattleDefaultAction(actionObject);
        }
    }
}
