using InventorySystem.Item.Interfaces;

namespace InventorySystem.Item
{
    public abstract class EquipmentItem : ItemBase, IEquipable
    {
        public abstract void Equip(Character character);

        public abstract void Unequip(Character character);
    }
}