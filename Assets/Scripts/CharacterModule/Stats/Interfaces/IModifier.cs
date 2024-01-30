using StatModule.Utility.Enums;

namespace StatModule.Interfaces
{
    public interface IModifier
    {
        public StatType StatType { get; set; }

        public ValueModifierType ValueModifierType { get; set; }

        public float Value { get; set; }
    }
}
