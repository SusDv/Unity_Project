using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;
using InventorySystem.Item.Interfaces;
using StatModule.Core;
using StatModule.Modifier;
using UnityEngine;

namespace InventorySystem.Item 
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Character/Items/Equipment")]
    public class Equipment : BaseItem, IItemAction, ITargetable
    {
        [field: SerializeField]
        public TargetType TargetType { get; set; }

        [field: SerializeField]
        public TargetSearchType TargetSearchType { get; set; }

        [field: SerializeField]
        [field: Range(1, 3)]
        public int TargetCount { get; set; } = 1;

        public void PerformAction(Character character)
        {
            if (character.GetCharacterWeapon().HaveWeapon)
            {
                UnequipItem(character);
            } 
            else 
            {
                EquipItem(character);
            }
            character.GetCharacterWeapon().HaveWeapon = !character.GetCharacterWeapon().HaveWeapon;
        }

        private void EquipItem(Character character) 
        {
            Stats characterStats = character.GetCharacterStats();
            
            foreach (BaseStatModifier baseStatModifier in StatModifiers.BaseModifiers) 
            {
                characterStats.AddStatModifier(baseStatModifier.Clone() as BaseStatModifier);
            }
        }
        
        private void UnequipItem(Character character) 
        {
            Stats characterStats = character.GetCharacterStats();

            characterStats.ApplyStatModifiersByCondition((statModifier) => statModifier.SourceID == ID);
        }
    }
}