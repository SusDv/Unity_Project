using System;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
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
            ModifierCapType modifierCapType,
            float value) : base (statType, valueModifierType, modifierCapType,value) {}

        public override void Modify(Stat statToModify, Action<BaseStatModifier> addModifierCallback,
            Action<BaseStatModifier> removeModifierCallback)
        {
            ValueModifierProcessor.ModifyStatValue(statToModify, this);
        }

        public static InstantStatModifier GetInstantStatModifierInstance(
            StatType statType, 
            ValueModifierType valueModifierType, 
            ModifierCapType modifierCapType,
            float value) 
        {
            return new InstantStatModifier(statType, valueModifierType, modifierCapType, value);
        }

        public override object Clone()
        {
            return new InstantStatModifier(StatType, ValueModifierType, ModifierCapType, Value);
        }

        public override bool Equals(BaseStatModifier other)
        {
            return other.Value == Value
                && other.ValueModifierType == ValueModifierType
                && other.SourceID == SourceID
                && other.ModifierCapType == ModifierCapType
                && other.StatType == StatType;
        }
    }
}
