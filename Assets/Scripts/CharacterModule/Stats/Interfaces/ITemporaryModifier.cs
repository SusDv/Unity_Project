using BattleModule.Utility;
using CharacterModule.Utility;

namespace CharacterModule.Stats.Interfaces
{
    public interface ITemporaryModifier<T> : IModifier<T>
    {
        public StatusEffectType StatusEffectType { get; }
        
        public int Duration { get; set; }
        
        public BattleTimer BattleTimer { get; set; }
    }
}