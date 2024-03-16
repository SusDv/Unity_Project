using System;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.Utility.Enums;
using Utility;

namespace CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifiers
{
    public abstract class TemporaryModifier
    {
        public abstract TemporaryStatModifierType TemporaryStatModifierType { get; }

        protected TemporaryStatModifier TemporaryStatModifier;
        
        protected Ref<float> ValueToModify;

        private Action<BaseStatModifier> _removeModifierCallback;
        
        public virtual TemporaryModifier Init(
            TemporaryStatModifier modifier,
            Ref<float> valueToModify,
            Action<BaseStatModifier> removeModifierCallback)
        {
            TemporaryStatModifier = modifier;
            ValueToModify = valueToModify;
            
            _removeModifierCallback = removeModifierCallback;

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
                _removeModifierCallback?.Invoke(TemporaryStatModifier);
            }
        }
    }
}