using System;
using BattleModule.Utility;
using CharacterModule.Utility;

namespace CharacterModule.Stats.Interfaces
{
    public interface ITemporaryModifier<T> : IModifier<T>
    {
        public TemporaryEffectType TemporaryEffectType { get; }
        
        public int Duration { get; set; }
        
        public BattleTimer BattleTimer { get; set; }

        public void SetRemoveCallback(Action<IModifier<T>> callback);
    }
}