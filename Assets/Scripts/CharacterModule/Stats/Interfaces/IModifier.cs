using System;
using CharacterModule.Utility.Stats;

namespace CharacterModule.Stats.Interfaces
{
    public interface IModifier<T> : IEquatable<IModifier<T>>
    {
        public T Type { get; }

        public ModifierData ModifierData { get; }
        
        public bool IsNegative { get; }
        
        public void OnRemove();
        
        public void OnAdded();
        
        public IModifier<T> Clone();
    }
}