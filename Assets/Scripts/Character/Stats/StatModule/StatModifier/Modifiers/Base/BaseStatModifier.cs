using System;
using UnityEngine;
using StatModule.Interfaces;
using StatModule.Utility.Enums;

namespace StatModule.Modifier
{
    [Serializable]
    public abstract class BaseStatModifier : IModifier, ICloneable
    {
        protected BaseStatModifier(
            StatType statType, 
            ValueModifierType valueModifierType,
            float value) 
        {
            StatType = statType;
            ValueModifierType = valueModifierType;
            Value = value;
        }

        [field: SerializeField]
        public StatType StatType { get; set; }

        [field: SerializeField]
        public ValueModifierType ValueModifierType { get; set; }

        [field: SerializeField]
        public float Value { get; set; }

        public abstract void Modify(IStat statToModify, Action<BaseStatModifier> addModifierCallback, Action<BaseStatModifier> removeModifierCallback);
        
        public abstract object Clone();

        public static BaseStatModifier operator -(BaseStatModifier baseModifier) 
        {
            baseModifier.Value = -baseModifier.Value;

            return baseModifier;
        }
    }
}
