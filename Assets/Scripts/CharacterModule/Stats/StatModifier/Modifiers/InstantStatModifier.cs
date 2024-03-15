using System;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using StatModule.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class InstantStatModifier : BaseStatModifier
    {
        private InstantStatModifier(
            StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValue modifiedValue,
            float value) : base (statType, valueModifierType, modifiedValue,value) {}

        public override void Modify(Stat statToModify, Action<BaseStatModifier> addModifierCallback,
            Action<BaseStatModifier> removeModifierCallback)
        {
            ValueModifierProcessor.ModifyStatValue(GetRefValue(statToModify), this);
        }

        public static InstantStatModifier GetInstantStatModifierInstance(
            StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValue modifiedValue,
            float value) 
        {
            return new InstantStatModifier(statType, valueModifierType, modifiedValue, value);
        }

        public override object Clone()
        {
            return new InstantStatModifier(StatType, ValueModifierType, ModifiedValue, Value);
        }

        public override bool Equals(BaseStatModifier other)
        {
            return other.Value == Value
                && other.ValueModifierType == ValueModifierType
                && other.SourceID == SourceID
                && other.ModifiedValue == ModifiedValue
                && other.StatType == StatType;
        }
    }
}
