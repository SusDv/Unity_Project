using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers.Effects.Base
{
    public abstract class TemporaryEffect
    {
        public abstract TemporaryEffectType TemporaryEffectType { get; }

        protected ITemporaryModifier TemporaryModifier;

        public virtual TemporaryEffect Init(ITemporaryModifier modifier)
        {
            TemporaryModifier = modifier;
            
            return this;
        }

        public virtual void Modify()
        {
            DecreaseDuration();
        }

        protected virtual void DecreaseDuration()
        {
            TemporaryModifier.Duration--;
        }

        public virtual void Remove()
        { }
    }
}