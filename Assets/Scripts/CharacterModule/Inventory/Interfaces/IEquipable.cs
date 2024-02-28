using CharacterModule;

namespace InventorySystem.Item.Interfaces
{
    public interface IEquipable
    {
        public void Equip(Character character);

        public void Unequip(Character character);
    }
}
