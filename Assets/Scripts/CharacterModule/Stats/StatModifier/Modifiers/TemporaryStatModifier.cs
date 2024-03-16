using System;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifiers;
using CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifiers.Base;
using CharacterModule.Stats.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class TemporaryStatModifier : BaseStatModifier 
    {
        private TemporaryStatModifier(StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValueType modifiedValueType,
            TemporaryStatModifierType temporaryStatModifierType,
            StatModifierTier temporaryStatModifierTier,
            float value,
            int duration) : base(statType, valueModifierType, modifiedValueType, value) 
        {
            TemporaryStatModifierType = temporaryStatModifierType;
            Duration = duration;
        }

        [field: SerializeField]
        public TemporaryStatModifierType TemporaryStatModifierType { get; private set; }

        [field: SerializeField]
        public StatModifierTier StatModifierTier { get; private set; }

        [field: SerializeField]
        public int Duration { get; set; }

        private TemporaryModifier _modifierProcessor;

        public override void Init(Stat statToModify, 
            Action<BaseStatModifier> addModifierCallback,
            Action<BaseStatModifier> removeModifierCallback)
        {
            base.Init(statToModify, addModifierCallback, removeModifierCallback);
            
            _modifierProcessor = TemporaryModifierProcessor.GetModifier(TemporaryStatModifierType)
                .Init(this, ValueToModify);
        }

        public override void Modify()
        {
            _modifierProcessor.Modify();
        }

        public static TemporaryStatModifier GetTemporaryStatModifierInstance(
            StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValueType modifiedValueType,
            TemporaryStatModifierType temporaryStatModifierType,
            StatModifierTier statModifierTier,
            float value,
            int duration)
        {
            return new TemporaryStatModifier(statType, valueModifierType, modifiedValueType, temporaryStatModifierType, statModifierTier, value, duration);
        }

        public override object Clone()
        {
            return  new TemporaryStatModifier(StatType, ValueModifierType, ModifiedValueType, TemporaryStatModifierType, StatModifierTier, Value, Duration);
        }

        public override bool Equals(BaseStatModifier other)
        {
            return other != null && 
                   Mathf.RoundToInt(other.Value - Value) == 0
                   && other.ValueModifierType == ValueModifierType
                   && other.SourceID == SourceID
                   && other.StatType == StatType
                   && ((TemporaryStatModifier)other).TemporaryStatModifierType == TemporaryStatModifierType;
        }
    }
}
