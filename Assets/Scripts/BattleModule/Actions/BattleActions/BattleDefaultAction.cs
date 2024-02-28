using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Utility;
using CharacterModule;
using CharacterModule.Stats.Base;
using InventorySystem.Item;
using JetBrains.Annotations;
using StatModule.Utility.Enums;

namespace BattleModule.Actions.BattleActions
{
    [UsedImplicitly]
    public class BattleDefaultAction : BattleAction 
    {
        protected override string ActionName => "Weapon attack";

        public override void PerformAction(StatManager source, List<Character> targets)
        {
            var characterWeapon = BattleActionContext.ActionObject as WeaponItem;

            foreach(var character in targets) 
            {
                var target = character.GetCharacterStats();

                float damage = -BattleAttackDamageProcessor.CalculateAttackDamage(
                    source.GetStatInfo(StatType.ATTACK).FinalValue,
                    target.GetStatInfo(StatType.DEFENSE).FinalValue);

                target.AddStatModifier(StatType.HEALTH,
                    damage);
            }

            source.AddStatModifier(StatType.BATTLE_POINTS, characterWeapon.BattlePoints);
        }
    }
}
