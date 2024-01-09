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

        private bool _modified;

        public override void Modify(IStat statToModify, Action<BaseStatModifier> modifierCallback)
        {
            if (!_modified)
            {
                ValueModifierProcessor.ModifyStatValue(statToModify, this);

                _modified = true;
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
    }
}
