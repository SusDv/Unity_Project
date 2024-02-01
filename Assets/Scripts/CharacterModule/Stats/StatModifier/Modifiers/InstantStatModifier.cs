using System;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using StatModule.Interfaces;
using StatModule.Modifier.ValueModifier;
using StatModule.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class InstantStatModifier : BaseStatModifier
    {
        private InstantStatModifier(
            StatType statType, 
            ValueModifierType valueModifierType, 
            float value) : base (statType, valueModifierType, value) {}

        public override void Modify(IStat statToModify, Action<BaseStatModifier> addModifierCallback, Action<BaseStatModifier> removeModifierCallback)
        {
            ValueModifierProcessor.ModifyStatValue(statToModify, this);
        }

        public static InstantStatModifier GetInstantStatModifierInstance(
            StatType statType,
            ValueModifierType valueModifierType,
            float value) 
        {
            return new InstantStatModifier(statType, valueModifierType, value);
        }

        public override object Clone()
        {
            return new InstantStatModifier(StatType, ValueModifierType, Value);
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
