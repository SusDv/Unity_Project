using System;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.Utility.Enums;
using Utility;

namespace CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifiers.Base
{
    public abstract class TemporaryModifier
    {
        public abstract TemporaryStatModifierType TemporaryStatModifierType { get; }

        protected TemporaryStatModifier TemporaryStatModifier;
        
        protected Ref<float> ValueToModify;
        
        public virtual TemporaryModifier Init(
            TemporaryStatModifier modifier,
            Ref<float> valueToModify)
        {
            if (TemporaryStatModifier != null)
            {
                if (modifier.Equals(TemporaryStatModifier))
                {
                    TemporaryStatModifier.Remove();
                }
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