using CharacterModule.Stats.Utility;

namespace CharacterModule.Stats.Interfaces
{
    public interface IModifier
    {
        public ModifierData ModifierData { get; }
        
        public void OnRemove();
        
        public void OnAdded();
        
        public IModifier Clone();
    }
}