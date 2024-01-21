using System;
using StatModule.Interfaces;
using StatModule.Modifier.ValueModifier;
using StatModule.Utility.Enums;

namespace StatModule.Modifier
{
    [Serializable]
    public class PermanentStatModifier : BaseStatModifier
    {
        private PermanentStatModifier(
            StatType statType,
            ValueModifierType valueModifierType,
            float value) : base(statType, valueModifierType, value) { }

        public override void Modify(IStat statToModify, Action<BaseStatModifier> addModifierCallback, Action<BaseStatModifier> removeModifierCallback)
        {
            if (!_isModified)
            {
                ValueModifierProcessor.ModifyStatValue(statToModify, this);

                _isModified = true;

                addModifierCallback?.Invoke(this);
            }
            else 
            {
                ValueModifierProcessor.ModifyStatValue(statToModify, -this);

                removeModifierCallback?.Invoke(this);
            }
        }

        public static PermanentStatModifier GetPermanentStatModifierInstance(
            StatType statType,
            ValueModifierType valueModifierType,
            float value)
        {
            return new PermanentStatModifier(statType, valueModifierType, value);
        }

        public override object Clone()
        {
            return new PermanentStatModifier(StatType, ValueModifierType, Value);
        }

        public override bool Equals(BaseStatModifier other)
        {
            return other.Value == Value
                && other.ValueModifierType == ValueModifierType
                && other.SourceID == SourceID
                && other.StatType == StatType;
        }
    }
}
