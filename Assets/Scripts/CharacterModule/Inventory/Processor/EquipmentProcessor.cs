using CharacterModule.Inventory.Interfaces;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.Modifiers.Containers;

namespace CharacterModule.Inventory.Processor
{
    public class EquipmentProcessor : IEquipment
    {
        private readonly int _sourceID;
        
        private readonly StatModifiers _statModifiers;
        
        public EquipmentProcessor(int sourceID,
            StatModifiers statModifiers)
        {
            _sourceID = sourceID;
            
            _statModifiers = statModifiers;
        }

        public void Equip(StatsController character)
        {
            foreach (var modifier in _statModifiers.GetModifiers().modifiers)
            {
                character.AddModifier(modifier);
            }
            
            foreach (var temporaryModifier in _statModifiers.GetModifiers().temporaryModifiers)
            {
                character.AddModifier(temporaryModifier);
            }
        }

        public void Unequip(StatsController character)
        {
            character.RemoveModifiersOnCondition((statModifier) => statModifier.ModifierData.SourceID == _sourceID);
        }
    }
}