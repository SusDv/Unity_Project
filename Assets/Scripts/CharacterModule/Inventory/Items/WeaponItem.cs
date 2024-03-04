using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;
using CharacterModule;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using InventorySystem.Item.Interfaces;
using StatModule.Modifier;
using UnityEngine;

namespace InventorySystem.Item
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Character/Items/Equipment/Weapon")]
    public class WeaponItem : EquipmentItem, IEquipable, ITargeting 
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
            StatManager characterStatManager = character.GetCharacterStats();

            if (character.GetCharacterWeapon().HaveWeapon)
            {
                Unequip(character);
            }
            else
            {
                foreach (BaseStatModifier baseStatModifier in StatModifiers.BaseModifiers)
                {
                    characterStatManager.ApplyStatModifier(baseStatModifier.Clone() as BaseStatModifier);
                }
            }

            character.GetCharacterWeapon().HaveWeapon = !character.GetCharacterWeapon().HaveWeapon;
        }

        public override void Unequip(Character character)
        {
            StatManager characterStatManager = character.GetCharacterStats();

            characterStatManager.ApplyStatModifiersByCondition((statModifier) => statModifier.SourceID == ID);
        }
    }
}