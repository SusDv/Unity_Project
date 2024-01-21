using StatModule.Utility.Enums;

namespace StatModule.Interfaces
{
    public interface IStat
    {
        public StatType StatType { get; set; }

        public float BaseValue { get; set; }

        public float BaseValuePrescaler { get; set; }

        public float FinalValue { get; set; }
    }
}
