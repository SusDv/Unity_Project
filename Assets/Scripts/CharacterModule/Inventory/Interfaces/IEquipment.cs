using CharacterModule.Stats.Managers;

namespace CharacterModule.Inventory.Interfaces
{
    public interface IEquipment
    {
        public void Equip(StatsController character);

        public void Unequip(StatsController character);
    }
}
