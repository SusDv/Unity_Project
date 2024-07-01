using System;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.ValueModifier.Processor;
using CharacterModule.Utility.Stats;
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
            
        }

        public abstract IModifier<T> Clone();

        public abstract bool Equals(IModifier<T> modifier);
    }
}