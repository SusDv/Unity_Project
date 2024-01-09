using InventorySystem.Inventory.Interfaces;
using InventorySystem.Item;
using System;

namespace InventorySystem.Core 
{
    [Serializable]
    public struct InventoryItem
    {
        public BaseItem inventoryItem;

        public int amount;

        public static InventoryItem GetEmptyItem()
        {
            return new InventoryItem
            {
                inventoryItem = null,
                amount = 0
            };
        }
        public InventoryItem ChangeAmount(int amount)
        {
            return new InventoryItem
            {
                inventoryItem = this.inventoryItem,
                amount = amount
            };
        }
    }
}