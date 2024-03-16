using System;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class InstantStatModifier : BaseStatModifier
    {
        private InstantStatModifier(
            StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValueType modifiedValueType,
            float value) : base (statType, valueModifierType, modifiedValueType,value) {}

        public override void Init(Stat statToModify, Action<BaseStatModifier> addModifierCallback, Action<BaseStatModifier> removeModifierCallback)
        {
            base.Init(statToModify, addModifierCallback, removeModifierCallback);
            
            Modify();
        }

        public override void Modify()
        {
            ValueModifierProcessor.ModifyStatValue(ValueToModify, this);
                
            Remove();
        }

        public static InstantStatModifier GetInstantStatModifierInstance(
            StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValueType modifiedValueType,
            float value) 
        {
            return new InstantStatModifier(statType, valueModifierType, modifiedValueType, value);
        }

        public override object Clone()
        {
            return new InstantStatModifier(StatType, ValueModifierType, ModifiedValueType, Value);
        }

        public override bool Equals(BaseStatModifier other)
        {
            return other.Value == Value
                && other.ValueModifierType == ValueModifierType
                && other.SourceID == SourceID
                && other.ModifiedValueType == ModifiedValueType
                && other.StatType == StatType;
        }
    }
}
