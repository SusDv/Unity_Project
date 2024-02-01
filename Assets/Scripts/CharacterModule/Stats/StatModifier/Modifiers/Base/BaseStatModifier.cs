using System;
using StatModule.Interfaces;
using StatModule.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier.Modifiers.Base
{
    [Serializable]
    public abstract class BaseStatModifier : IModifier, ICloneable, IEquatable<BaseStatModifier>
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

        protected bool Initialized;

        [field: SerializeField]
        public StatType StatType { get; set; }

        [field: SerializeField]
        public ValueModifierType ValueModifierType { get; set; }

        [field: SerializeField]
        public float Value { get; set; }

        public int SourceID { get; set; }

        public abstract void Modify(IStat statToModify, Action<BaseStatModifier> addModifierCallback, Action<BaseStatModifier> removeModifierCallback);
        
        public abstract object Clone();

        public abstract bool Equals(BaseStatModifier other);

        public static BaseStatModifier operator -(BaseStatModifier baseModifier) 
        {
            baseModifier.Value = -baseModifier.Value;

            return baseModifier;
        }
    }
}
