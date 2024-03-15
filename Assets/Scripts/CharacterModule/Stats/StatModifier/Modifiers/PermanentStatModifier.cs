using System;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using StatModule.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class PermanentStatModifier : BaseStatModifier
    {
        private PermanentStatModifier(StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValue modifiedValue,
            float value) : base(statType, valueModifierType, modifiedValue, value) { }

        public override void Modify(Stat statToModify, Action<BaseStatModifier> addModifierCallback,
            Action<BaseStatModifier> removeModifierCallback)
        {
            if (!IsApplied)
            {
                ValueModifierProcessor.ModifyStatValue(GetRefValue(statToModify), this);

                IsApplied = true;

                addModifierCallback?.Invoke(this);
                
                return;
            }
            
            ValueModifierProcessor.ModifyStatValue(GetRefValue(statToModify), -this);

            removeModifierCallback?.Invoke(this);
        }

        public static PermanentStatModifier GetPermanentStatModifierInstance(
            StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValue modifiedValue,
            float value)
        {
            return new PermanentStatModifier(statType, valueModifierType, modifiedValue, value);
        }

        public override object Clone()
        {
            return new PermanentStatModifier(StatType, ValueModifierType, ModifiedValue, Value);
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
