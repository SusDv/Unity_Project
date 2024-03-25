using System;
using CharacterModule.Inventory.Items.Base;

namespace CharacterModule.Inventory 
{
    [Serializable]
    public struct InventoryItem
    {
        public ItemBase Item;

        public int Amount;

        public static InventoryItem GetEmptyItem()
        {
            return new InventoryItem
            {
                Item = null,
                Amount = 0
            };
        }
        public InventoryItem ChangeAmount(int amount)
        {
            return new InventoryItem
            {
                Item = Item,
                Amount = amount
            };
        }
    }
}