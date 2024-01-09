using InventorySystem.Item.Interfaces;
using StatModule.Core;
using StatModule.Modifier;
using UnityEngine;

namespace InventorySystem.Item 
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Character/Items/Consumable")]
    public class Consumable : BaseItem, IItemAction
    {
        public void PerformAction(Character character)
        {
            Stats characterStats = character.GetStats();

            foreach (BaseStatModifier modifier in BaseModifiers.BaseModifiers)
            {
                characterStats.ModifyStat(modifier);
            }

            OnItemAction?.Invoke(this);
        }
    }
}