using System;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Utility;
using UnityEngine;

namespace CharacterModule.Stats.Modifiers.Base
{
    [Serializable]
    public abstract class InstantModifier<T> : IModifier<T>
    {
        protected InstantModifier(T type,
            ModifierData modifierData)
        {
            Type = type;
            
            ModifierData = modifierData;
        }

        
        public T Type { get; }

        [field: SerializeField]
        public ModifierData ModifierData { get; private set; }

        public virtual void OnAdded()
        {
            ValueModifierProcessor.ModifyValue(ModifierData);
        }
        
        public virtual void OnRemove()
        {
            
        }

        public abstract IModifier<T> Clone();

        public abstract bool Equals(IModifier<T> modifier);
    }
}