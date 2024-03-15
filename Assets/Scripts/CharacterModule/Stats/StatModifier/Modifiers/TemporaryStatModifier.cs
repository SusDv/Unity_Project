using System;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using StatModule.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class TemporaryStatModifier : BaseStatModifier 
    {
        private TemporaryStatModifier(StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValue modifiedValue,
            float value,
            TemporaryStatModifierType temporaryStatModifierType,
            StatModifierTier temporaryStatModifierTier,
            int duration) : base(statType, valueModifierType, modifiedValue, value) 
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

        private int _localCycleTimer;

        public override void Modify(Stat statToModify,
            Action<BaseStatModifier> addModifierCallback,
            Action<BaseStatModifier> removeModifierCallback) 
        {
            if (!IsApplied)
            {
                _localCycleTimer = LocalCycle;
                
                if (TemporaryStatModifierType.Equals(TemporaryStatModifierType.APPLIED_ONCE))
                {
                    ValueModifierProcessor.ModifyStatValue(GetRefValue(statToModify), this);
                }

                IsApplied = true;
                
                addModifierCallback?.Invoke(this);
                
                return;
            }
            
            if (TemporaryStatModifierType != TemporaryStatModifierType.APPLIED_EVERY_CYCLE)
            {
                Duration--;
            }
            else
            {
                if (--_localCycleTimer == 0)
                {
                    Duration--;
                    
                    ValueModifierProcessor.ModifyStatValue(GetRefValue(statToModify), this);
                }
            }
                
            if(TemporaryStatModifierType.Equals(TemporaryStatModifierType.APPLIED_EVERY_TURN))
            {
                ValueModifierProcessor.ModifyStatValue(GetRefValue(statToModify), this);
            }

            if (Duration != 0)
            {
                return;
            }
            
            if (TemporaryStatModifierType.Equals(TemporaryStatModifierType.APPLIED_ONCE))
            {
                ValueModifierProcessor.ModifyStatValue(GetRefValue(statToModify), -this);
            }

            removeModifierCallback?.Invoke(this);
        }
        
        public static TemporaryStatModifier GetTemporaryStatModifierInstance(
            StatType statType, 
            ValueModifierType valueModifierType, 
            ModifiedValue modifiedValue,
            float value,
            TemporaryStatModifierType temporaryStatModifierType,
            StatModifierTier statModifierTier,
            int duration)
        {
            return new TemporaryStatModifier(statType, valueModifierType, modifiedValue, value, temporaryStatModifierType, statModifierTier, duration);
        }

        public override object Clone()
        {
            return  new TemporaryStatModifier(StatType, ValueModifierType, ModifiedValue, Value, TemporaryStatModifierType, StatModifierTier, Duration);
        }

        public override bool Equals(BaseStatModifier other)
        {
            return other.Value == Value
                   && other.ValueModifierType == ValueModifierType
                   && other.SourceID == SourceID
                   && other.StatType == StatType
                   && ((TemporaryStatModifier)other).TemporaryStatModifierType == TemporaryStatModifierType;
        }
    }
}
