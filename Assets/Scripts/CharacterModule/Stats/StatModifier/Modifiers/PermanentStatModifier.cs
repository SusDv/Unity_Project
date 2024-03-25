using System;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class PermanentStatModifier : BaseStatModifier
    {
        private PermanentStatModifier(StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValueType modifiedValueType,
            float value) : base(statType, valueModifierType, modifiedValueType, value) { }

        public override void Init(Stat statToModify, Action<BaseStatModifier> addModifierCallback, Action<BaseStatModifier> removeModifierCallback)
        {
            base.Init(statToModify, addModifierCallback, removeModifierCallback);
            
            Modify();
        }

        public override void Modify()
        {
            ValueModifierProcessor.ModifyStatValue(ValueToModify, this);
        }

        public override void Remove()
        {
            base.Remove();
            
            ValueModifierProcessor.ModifyStatValue(ValueToModify, -this);
        }

        public static PermanentStatModifier GetPermanentStatModifierInstance(
            StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValueType modifiedValueType,
            float value)
        {
            return new PermanentStatModifier(statType, valueModifierType, modifiedValueType, value);
        }

        public override object Clone()
        {
            return new PermanentStatModifier(StatType, ValueModifierType, ModifiedValueType, Value);
        }
    }
}
