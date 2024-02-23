using System;
using CharacterModule.Stats.Base;
using StatModule.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier.Modifiers.Base
{
    [Serializable]
    public abstract class BaseStatModifier : ICloneable, IEquatable<BaseStatModifier>
    {
        protected BaseStatModifier(
            StatType statType, 
            ValueModifierType valueModifierType,
            ModifierCapType modifierCapType,
            float value) 
        {
            StatType = statType;
            ValueModifierType = valueModifierType;
            ModifierCapType = modifierCapType;
            Value = value;
        }

        protected bool Initialized { get; set; }

        [field: SerializeField]
        public StatType StatType { get; set; }

        [field: SerializeField]
        public ValueModifierType ValueModifierType { get; set; }

        [field: SerializeField]
        public ModifierCapType ModifierCapType { get; set; }

        [field: SerializeField]
        public float Value { get; set; }

        public int SourceID { get; set; }

        public bool IsNegative => Value < 0;

        public abstract void Modify(Stat statToModify, 
            Action<BaseStatModifier> addModifierCallback,
            Action<BaseStatModifier> removeModifierCallback);
        
        public abstract object Clone();

        public abstract bool Equals(BaseStatModifier other);

        public static BaseStatModifier operator -(BaseStatModifier baseModifier) 
        {
            baseModifier.Value = -baseModifier.Value;

            return baseModifier;
        }
    }
}
