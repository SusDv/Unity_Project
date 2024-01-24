using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;
using InventorySystem.Item.Interfaces;
using StatModule.Base;
using StatModule.Modifier;
using UnityEngine;

namespace InventorySystem.Item
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Character/Items/Equipment/Weapon")]
    public class WeaponItem : EquipmentItem, IEquipable, ITargetable 
    {
        [field: SerializeField]
        public TargetType TargetType { get; set; }

        [field: SerializeField]
        public TargetSearchType TargetSearchType { get; set; }

        [field: SerializeField]
        [field: Range(1, 5)]
        public int MaxTargetsCount { get; set; } = 1;

        public override void Equip(Character character)
        {
            Stats characterStats = character.GetCharacterStats();

            if (character.GetCharacterWeapon().HaveWeapon)
            {
                Unequip(character);
            }
            else
            {
                foreach (BaseStatModifier baseStatModifier in StatModifiers.BaseModifiers)
                {
                    characterStats.AddStatModifier(baseStatModifier.Clone() as BaseStatModifier);
                }
            }

            character.GetCharacterWeapon().HaveWeapon = !character.GetCharacterWeapon().HaveWeapon;
        }

        public override void Unequip(Character character)
        {
            Stats characterStats = character.GetCharacterStats();

            characterStats.ApplyStatModifiersByCondition((statModifier) => statModifier.SourceID == ID);
        }
    }
}