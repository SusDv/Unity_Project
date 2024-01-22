using System;
using System.Collections.Generic;
using InventorySystem.Core;

namespace InventorySystem.Intefaces
{
    public interface IBattleInvetory
    {
        public Action<List<InventoryItem>> OnInventoryChanged { get; set; }
        public List<InventoryItem> GetBattleInventory();
    }
}
