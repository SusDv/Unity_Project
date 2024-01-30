using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Utility;
using InventorySystem.Item;
using StatModule.Interfaces;
using StatModule.Utility.Enums;

namespace BattleModule.Actions.BattleActions
{
    public class BattleDefaultAction : BattleAction 
    {
        protected override string ActionName => "Weapon attack";

        public BattleDefaultAction() 
            : base()
        {}

        private BattleDefaultAction(object actionObject) 
            : base(actionObject) 
        {}

        public override void PerformAction(IHaveStats source, List<Character> targets)
        {
            WeaponItem characterWeapon = BattleActionContext.ActionObject as WeaponItem;

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
