using System;
using System.Collections.Generic;
using System.Linq;
using CharacterModule.Inventory.Items;
using CharacterModule.Inventory.Items.Base;
using UnityEngine;

namespace CharacterModule.Inventory 
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Character/Inventory/Inventory")]
    public class InventoryBase : ScriptableObject
    {
        public Action<InventoryBase> OnInventoryChanged { get; set; } = delegate { };

        private List<InventoryItem> _inventoryItems;

        public void InitializeInventory()
        {
            _inventoryItems = new List<InventoryItem>();
        }

        public void AddItem(ItemBase item, int amount)
        {
            (int index, var existingItem) = FindItem(item);

            if (existingItem.Equals(InventoryItem.GetEmptyItem()))
            {
                _inventoryItems.Add(new InventoryItem
                {
                    Item = item,
                    Amount = amount
                });

                if (item is ConsumableItem consumableItem)
                {
                    consumableItem.OnConsumableUsed += ConsumableItemUsed;
                }
            }
            else
            {
                if (existingItem.Item.IsStackable)
                {
                    _inventoryItems[index] = existingItem.ChangeAmount(amount + existingItem.Amount);
                }
                else
                {
                    _inventoryItems.Add(new InventoryItem
                    {
                        Item = item,
                        Amount = amount
                    });
                }
            }

            OnInventoryChanged?.Invoke(this);
        }

        public void RemoveItem(InventoryItem item)
        {
            _inventoryItems.RemoveAt(_inventoryItems.IndexOf(item));

            OnInventoryChanged?.Invoke(this);
        }
        
        public (int index, InventoryItem item) FindItem(ItemBase itemBase)
        {
            for (var i = 0; i < _inventoryItems.Count; i++)
            {
                var currentItem = _inventoryItems[i];
                
                if (currentItem.Item.ID == itemBase.ID)
                {
                    return (i, currentItem);
                }
            }
            return (-1, InventoryItem.GetEmptyItem());
        }

        public List<InventoryItem> GetBattleInventory()
        {
            return _inventoryItems.Where(item => item.Item.GetType() == typeof(ConsumableItem)).ToList();
        }

        private void ConsumableItemUsed(ItemBase inventoryItem)
        {
            (int index, var item) = FindItem(inventoryItem);

            if (item.Item.IsStackable)
            {
                if (item.Amount == 1)
                {
                    RemoveItem(item);

                    return;
                }

                _inventoryItems[index] = item.ChangeAmount(_inventoryItems[index].Amount - 1);
            }

            OnInventoryChanged?.Invoke(this);
        }
    }
}