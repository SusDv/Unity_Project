using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.Interfaces
{
    public interface IStatModifier
    {
        public StatType StatType { get; }
    }
}