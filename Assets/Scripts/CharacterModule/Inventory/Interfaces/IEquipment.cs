using CharacterModule.Stats.StatModifier.Manager;

namespace CharacterModule.Inventory.Interfaces
{
    public interface IEquipment
    {
        public void Equip(StatModifierManager character);

        public void Unequip(StatModifierManager character);
    }
}
