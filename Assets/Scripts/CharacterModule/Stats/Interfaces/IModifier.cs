using System;
using CharacterModule.Stats.Utility;

namespace CharacterModule.Stats.Interfaces
{
    public interface IModifier : IEquatable<IModifier>
    {
        public ModifierData ModifierData { get; }
        
        public void OnRemove();
        
        public void OnAdded();
        
        public IModifier Clone();
    }
}