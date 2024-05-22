using CharacterModule.Stats.Managers;

namespace CharacterModule.Inventory.Interfaces
{
    public interface IEquipment
    {
        public void Equip(StatManager character);

        public void Unequip(StatManager character);
    }
}
