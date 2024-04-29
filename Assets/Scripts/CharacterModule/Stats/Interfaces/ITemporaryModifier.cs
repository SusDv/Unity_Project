using CharacterModule.Stats.StatModifier.Modifiers.Effects.Base;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.Interfaces
{
    public interface ITemporaryModifier : IModifier
    {
        public TemporaryEffectType TemporaryEffectType { get; }

        public TemporaryEffect TemporaryEffect { set; }
        
        public int Duration { get; set; }

        public int LocalCycle { get; }

        public void Modify();
    }
}