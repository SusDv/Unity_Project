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
    public class InventorySystemBase : ScriptableObject, IBattleInvetory
    {
        public Action<List<InventoryItem>> OnInventoryChanged { get; set; } = delegate { };

        private List<InventoryItem> _inventoryItems;

        public void InitializeInventory()
        {
            _inventoryItems = new List<InventoryItem>();
        }

        public void AddItem(BaseItem item, int amount)
        {
            var existingItem = FindItem(item);

            item.OnItemAction = item.IsStackable ? StackableItemUsed : null;

            if (existingItem.item.Equals(InventoryItem.GetEmptyItem()))
            {
                _inventoryItems.Add(new InventoryItem
                {
                    inventoryItem = item,
                    amount = amount
                });
            }
            else
            {
                if (existingItem.item.inventoryItem.IsStackable)
                {
                    _inventoryItems[existingItem.index] = existingItem.item.ChangeAmount(amount + existingItem.item.amount);
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

        public void StackableItemUsed(BaseItem inventoryItem)
        {
            var itemToUse = FindItem(inventoryItem);

            if (itemToUse.item.inventoryItem.IsStackable)
            {
                if (itemToUse.item.amount == 1)
                {
                    RemoveItem(itemToUse.item);

                    return;
                }
                else
                {
                    _inventoryItems[itemToUse.index] = itemToUse.item.ChangeAmount(_inventoryItems[itemToUse.index].amount - 1);
                }
            }

            OnInventoryChanged?.Invoke(_inventoryItems);
        }
        
        public (int index, InventoryItem item) FindItem(BaseItem ItemBase)
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
    }
}