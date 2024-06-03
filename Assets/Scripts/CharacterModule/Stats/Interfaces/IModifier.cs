using System;
using CharacterModule.Utility.Stats;

namespace CharacterModule.Stats.Interfaces
{
    public interface IModifier : IEquatable<IModifier>
    {
        public ModifierData ModifierData { get; }
        
        public void OnRemove();
        
        public void OnAdded();
        
        public IModifier Clone();
    }

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