using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using InventorySystem.Item;
using InventorySystem.Intefaces;

namespace InventorySystem.Core 
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Character/Inventory/Inventory")]
    public class InventoryBase : ScriptableObject, IBattleInvetory
    {
        public Action<List<InventoryItem>> OnInventoryChanged { get; set; } = delegate { };

        private List<InventoryItem> _inventoryItems;

        public void InitializeInventory()
        {
            _inventoryItems = new List<InventoryItem>();
        }

        public void AddItem(ItemBase item, int amount)
        {
            var (index, existingItem) = FindItem(item);

            if (existingItem.Equals(InventoryItem.GetEmptyItem()))
            {
                _inventoryItems.Add(new InventoryItem
                {
                    inventoryItem = item,
                    amount = amount
                });

                if (item is ConsumableItem consumableItem)
                {
                    consumableItem.OnConsumableUsed += ConsumableItemUsed;
                }
            }
            else
            {
                if (existingItem.inventoryItem.IsStackable)
                {
                    _inventoryItems[index] = existingItem.ChangeAmount(amount + existingItem.amount);
                }
                else
                {
                    _inventoryItems.Add(new InventoryItem
                    {
                        inventoryItem = item,
                        amount = amount
                    });
                }
            }

            OnInventoryChanged?.Invoke(_inventoryItems);
        }

        public void RemoveItem(InventoryItem item)
        {
            _inventoryItems.RemoveAt(_inventoryItems.IndexOf(item));

            OnInventoryChanged?.Invoke(_inventoryItems);
        }
        
        public (int index, InventoryItem item) FindItem(ItemBase ItemBase)
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                InventoryItem currentItem = _inventoryItems[i];
                if (currentItem.inventoryItem.ID == ItemBase.ID)
                {
                    return (i, currentItem);
                }
            }
            return (-1, InventoryItem.GetEmptyItem());
        }

        public List<InventoryItem> GetBattleInventory()
        {
            return _inventoryItems.Where(item => item.inventoryItem.GetType().Equals(typeof(ConsumableItem))).ToList();
        }

        private void ConsumableItemUsed(ItemBase inventoryItem)
        {
            var (index, item) = FindItem(inventoryItem);

            if (item.inventoryItem.IsStackable)
            {
                if (item.amount == 1)
                {
                    RemoveItem(item);

                    return;
                }
                else
                {
                    _inventoryItems[index] = item.ChangeAmount(_inventoryItems[index].amount - 1);
                }
            }

            OnInventoryChanged?.Invoke(_inventoryItems);
        }
    }
}