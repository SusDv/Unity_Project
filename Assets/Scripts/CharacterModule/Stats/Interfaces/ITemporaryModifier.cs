using BattleModule.Utility;
using CharacterModule.Stats.StatModifier.Modifiers.Effects.Base;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.Interfaces
{
    public interface ITemporaryModifier : IModifier
    {
        public TemporaryEffectType TemporaryEffectType { get; }
        
        public int Duration { get; set; }
        
        public BattleTimer BattleTimer { get; set; }
    }
}