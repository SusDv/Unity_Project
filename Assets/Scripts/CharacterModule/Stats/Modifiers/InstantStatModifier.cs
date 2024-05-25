using System;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Modifiers.Base;
using CharacterModule.Utility;

namespace CharacterModule.Stats.Modifiers
{
    [Serializable]
    public class InstantStatModifier : InstantModifier<StatType>
    { 
        private InstantStatModifier(
            StatType statType,
            ModifierData modifierData) : base(statType, modifierData)
        { }
        
        public override IModifier<StatType> Clone()
        {
            return new InstantStatModifier(Type, ModifierData.Clone());
        }
        
        public static InstantStatModifier GetInstance(
            StatType statType, 
            float value)
        {
            var modifierData = new ModifierData
            {
                Value = value
            };

            return new InstantStatModifier(statType, modifierData);
        }

        private bool Equals(InstantStatModifier other)
        {
            return Type == other.Type && Equals(ModifierData, other.ModifierData);
        }

        public override bool Equals(IModifier<StatType> obj)
        {
            return Equals((InstantStatModifier) obj);
        }
    }
}
