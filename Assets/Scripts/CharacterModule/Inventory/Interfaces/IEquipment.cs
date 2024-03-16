using CharacterModule;
using CharacterModule.Stats.Managers;

namespace InventorySystem.Item.Interfaces
{
    public interface IEquipment
    {
        public void Equip(StatManager character);

        public void Unequip(StatManager character);
    }
}
