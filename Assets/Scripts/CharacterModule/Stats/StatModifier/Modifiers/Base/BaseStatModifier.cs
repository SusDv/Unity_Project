using System;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Utility.Enums;
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
            ModifiedValueType modifiedValueType,
            float value) 
        {
            StatType = statType;
            ValueModifierType = valueModifierType;
            ModifiedValueType = modifiedValueType;
            Value = value;
        }

        protected bool Initialized { get; set; }

        [field: SerializeField]
        public StatType StatType { get; set; }

        [field: SerializeField]
        public ValueModifierType ValueModifierType { get; set; }

        [field: SerializeField]
        public ModifiedValueType ModifiedValueType { get; set; }

        [field: SerializeField]
        public float Value { get; set; }

        public int SourceID { get; set; }

        public bool IsNegative => Value < 0;

        protected static int LocalCycle;

        protected Ref<float> ValueToModify;

        protected Action<BaseStatModifier> RemoveModifierCallback;
        
        public virtual void Init(Stat statToModify, 
            Action<BaseStatModifier> addModifierCallback,
            Action<BaseStatModifier> removeModifierCallback)
        {
            ValueToModify = GetRefValue(statToModify);

            RemoveModifierCallback = removeModifierCallback;
            
            addModifierCallback?.Invoke(this);
            
            Initialized = true;
        }

        public abstract void Modify();

        public virtual void Remove()
        {
            RemoveModifierCallback?.Invoke(this);
        }

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
        
        private Ref<float> GetRefValue(Stat statToModify)
        {
            return ModifiedValueType == ModifiedValueType.FINAL_VALUE
                ? new Ref<float>(() => statToModify.FinalValue, v => statToModify.FinalValue = v) :
                new Ref<float>(() => statToModify.MaxValue, v => statToModify.MaxValue = v);
        }
    }
}
