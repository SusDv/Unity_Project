using System;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifierEffects.Base;
using CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifierEffects.Processor;
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
            TemporaryEffectType temporaryEffectType,
            StatModifierTier temporaryStatModifierTier,
            float value,
            int duration) : base(statType, valueModifierType, modifiedValueType, value) 
        {
            TemporaryEffectType = temporaryEffectType;
            Duration = duration;
        }

        [field: SerializeField]
        public TemporaryEffectType TemporaryEffectType { get; private set; }

        [field: SerializeField]
        public StatModifierTier StatModifierTier { get; private set; }

        [field: SerializeField]
        public int Duration { get; set; }

        private TemporaryModifierEffect _temporaryModifierEffect;

        public override void Init(Stat statToModify, 
            Action<BaseStatModifier> addModifierCallback,
            Action<BaseStatModifier> removeModifierCallback)
        {
            base.Init(statToModify, addModifierCallback, removeModifierCallback);
            
            _temporaryModifierEffect = TemporaryModifierEffectProcessor.GetEffect(TemporaryEffectType)
                .Init(this, ValueToModify);
        }

        public override void Modify()
        {
            _temporaryModifierEffect.Modify();
        }

        public static TemporaryStatModifier GetTemporaryStatModifierInstance(
            StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValueType modifiedValueType,
            TemporaryEffectType temporaryEffectType,
            StatModifierTier statModifierTier,
            float value,
            int duration)
        {
            return new TemporaryStatModifier(statType, valueModifierType, modifiedValueType, temporaryEffectType, statModifierTier, value, duration);
        }

        public override object Clone()
        {
            return  new TemporaryStatModifier(StatType, ValueModifierType, ModifiedValueType, TemporaryEffectType, StatModifierTier, Value, Duration);
        }

        public override bool Equals(BaseStatModifier other)
        {
            return base.Equals(other) && ((TemporaryStatModifier)other).TemporaryEffectType == TemporaryEffectType;
        }
    }
}
