using CharacterModule;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Stats.Managers;
using InventorySystem.Item.Interfaces;

namespace InventorySystem.Item
{
    public abstract class EquipmentItem : ItemBase, IEquipment
    {
        public abstract void Equip(StatManager stats);

        public abstract void Unequip(StatManager stats);
    }
}