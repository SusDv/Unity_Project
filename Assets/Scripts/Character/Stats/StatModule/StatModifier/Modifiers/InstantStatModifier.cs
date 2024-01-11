using System;
using StatModule.Interfaces;
using StatModule.Modifier.ValueModifier;
using StatModule.Utility.Enums;
using UnityEngine;

namespace StatModule.Modifier
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
    }
}
