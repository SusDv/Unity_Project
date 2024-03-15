using System;
using CharacterModule.Stats.Base;
using StatModule.Utility.Enums;
using UnityEngine;
using Utility;

namespace CharacterModule.Stats.StatModifier.Modifiers.Base
{
    [Serializable]
    public abstract class BaseStatModifier : ICloneable, IEquatable<BaseStatModifier>
    {
        protected BaseStatModifier(
            StatType statType, 
            ValueModifierType valueModifierType,
            ModifiedValue modifiedValue,
            float value) 
        {
            StatType = statType;
            ValueModifierType = valueModifierType;
            ModifiedValue = modifiedValue;
            Value = value;
        }

        protected bool IsApplied { get; set; }

        [field: SerializeField]
        public StatType StatType { get; set; }

        [field: SerializeField]
        public ValueModifierType ValueModifierType { get; set; }

        [field: SerializeField]
        public ModifiedValue ModifiedValue { get; set; }

        [field: SerializeField]
        public float Value { get; set; }

        public int SourceID { get; set; }

        public bool IsNegative => Value < 0;

        protected static int LocalCycle;

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

        public static void SetLocalCycle(int localCycle)
        {
            LocalCycle = localCycle;
        }

        protected Ref<float> GetRefValue(Stat statToModify)
        {
            return ModifiedValue == ModifiedValue.FINAL_VALUE
                ? new Ref<float>(() => statToModify.FinalValue, v => statToModify.FinalValue = v) :
                new Ref<float>(() => statToModify.MaxValue, v => statToModify.MaxValue = v);
        }
    }
}
