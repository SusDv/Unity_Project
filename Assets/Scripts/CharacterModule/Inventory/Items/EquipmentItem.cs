using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Stats.Managers;

namespace CharacterModule.Inventory.Items
{
    public abstract class EquipmentItem : ItemBase, IEquipment
    {
        public abstract void Equip(StatManager stats);

        public abstract void Unequip(StatManager stats);
    }
}