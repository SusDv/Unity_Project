using CharacterModule.Weapon;
using InventorySystem.Item.Interfaces;
using StatModule.Core;
using StatModule.Modifier;
using System.Linq;
using UnityEngine;

namespace InventorySystem.Item 
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Character/Items/Equipment")]
    public class Equipment : BaseItem, IItemAction
    {
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
            
            foreach (BaseStatModifier baseStatModifier in BaseModifiers.BaseModifiers) 
            {
                characterStats.ApplyStatModifier(baseStatModifier.Clone() as BaseStatModifier);
            }
        }

        private void UnequipItem(Character character) 
        {
            Stats characterStats = character.GetCharacterStats();
            
            foreach (BaseStatModifier baseStatModifier in BaseModifiers.BaseModifiers) 
            {
                characterStats.ApplyStatModifier(characterStats.GetBaseStatModifiers()
                                .Where((statModifier) => statModifier.Equals(baseStatModifier))
                                    .First());
            }
        }
    }
}