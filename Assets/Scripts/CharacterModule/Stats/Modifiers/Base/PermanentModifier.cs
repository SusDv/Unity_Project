using System;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Utility.Stats;
using UnityEngine;

namespace CharacterModule.Stats.Modifiers.Base
{
    [Serializable]
    public abstract class PermanentModifier<T> : IModifier<T>
    {
        protected PermanentModifier(
            T type,
            ModifierData modifierData)
        {
            Type = type;
            
            ModifierData = modifierData;
        }
        
        [field: SerializeField]
        public T Type { get; private set; }
        
        [field: SerializeField]
        public ModifierData ModifierData { get; private set; }
        
        public bool IsNegative => ModifierData.Value < 0;
        
        public virtual void OnAdded()
        {
            ValueModifierProcessor.ModifyValue(ModifierData);
        }

        public virtual void OnRemove()
        {
            ValueModifierProcessor.ModifyValue(ModifierData.GetInverseModifier());
        }

        public abstract IModifier<T> Clone();

        protected virtual bool Equals(PermanentModifier<T> modifier)
        {
            return Equals(ModifierData, modifier.ModifierData);
        }

        public bool Equals(IModifier<T> obj)
        {
            return Equals((PermanentModifier<T>) obj);
        }
    }
}