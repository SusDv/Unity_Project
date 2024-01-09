using InventorySystem.Core;
using InventorySystem.Item;

namespace InventorySystem.Inventory.Interfaces
{
    public interface IInventoryAction
    {
        public void UseItem(Character target);
    }
}
