using StatModule.Modifier;
using StatModule.Utility.Enums;

namespace StatModule.Interfaces
{
    public interface IHaveStats
    {
        public void AddStatModifier(BaseStatModifier baseStatModifier);

        public void AddStatModifier(StatType statType, float value);

        public float GetStatFinalValue(StatType type);
    }
}
