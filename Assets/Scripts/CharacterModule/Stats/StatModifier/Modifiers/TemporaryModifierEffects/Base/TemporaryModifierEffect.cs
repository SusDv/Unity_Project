using CharacterModule.Stats.Utility.Enums;
using Utility;

namespace CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifierEffects.Base
{
    public abstract class TemporaryModifierEffect
    {
        public abstract TemporaryEffectType TemporaryEffectType { get; }

        protected TemporaryStatModifier TemporaryStatModifier;
        
        protected Ref<float> ValueToModify;
        
        public virtual TemporaryModifierEffect Init(
            TemporaryStatModifier modifier,
            Ref<float> valueToModify)
        {
            if (modifier.Equals(TemporaryStatModifier))
            {
                TemporaryStatModifier.Remove();
            }
            
            TemporaryStatModifier = modifier;
            
            ValueToModify = valueToModify;

            return this;
        }

        public virtual void Modify()
        {
            DecreaseDuration();
            
            RemoveModifier();
        }

        protected virtual void DecreaseDuration()
        {
            TemporaryStatModifier.Duration--;
        }

        protected virtual void RemoveModifier()
        {
            if (TemporaryStatModifier.Duration == 0)
            {
                TemporaryStatModifier.Remove();
            }
        }
    }
}