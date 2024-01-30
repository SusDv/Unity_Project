using System;
using BattleModule.Actions;
using StatModule.Interfaces;
using StatModule.Modifier;
using StatModule.Modifier.ValueModifier;
using StatModule.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class TemporaryStatModifier : BaseStatModifier 
    {
        private TemporaryStatModifier(
            StatType statType,
            ValueModifierType valueModifierType,
            float value,
            TemporaryStatModifierType temporaryStatModifierType,
            int duration) : base(statType, valueModifierType, value) 
        {
            TemporaryStatModifierType = temporaryStatModifierType;
            Duration = duration;
        }

        [field: SerializeField]
        public TemporaryStatModifierType TemporaryStatModifierType { get; private set; }

        [field: SerializeField]
        public StatModifierTier TemporaryStatModifierTier { get; private set; }

        [field: SerializeField]
        public int Duration { get; set; }

        public bool IsNegative => Value < 0;

        private int LocalCycleDuration { get; set; }

        public override void Modify(IStat statToModify, 
            Action<BaseStatModifier> addModifierCallback, 
            Action<BaseStatModifier> removeModifierCallback) 
        {
            if (!_isModified)
            {
                LocalCycleDuration = BattleEventManager.Instance.GetCurrentTurnCount();
                
                if (TemporaryStatModifierType.Equals(TemporaryStatModifierType.APPLIED_ONCE))
                {
                    ValueModifierProcessor.ModifyStatValue(statToModify, this);
                }

                _isModified = true;
                
                addModifierCallback?.Invoke(this);
            }
            else 
            {
                if (TemporaryStatModifierType != TemporaryStatModifierType.APPLIED_EVERY_CYCLE)
                {
                    Duration--;
                }
                else
                {
                    if (BattleEventManager.Instance.GetCurrentTurnCount() - LocalCycleDuration ==
                        BattleEventManager.Instance.GetMaximumTurnsInCycle())
                    {
                        Duration--;
                    }
                }
                
                if(TemporaryStatModifierType.Equals(TemporaryStatModifierType.APPLIED_EVERY_TURN))
                {
                    ValueModifierProcessor.ModifyStatValue(statToModify, this);
                }
            }

            if (Duration <= 0) 
            {
                if (TemporaryStatModifierType.Equals(TemporaryStatModifierType.APPLIED_ONCE)) 
                {
                    ValueModifierProcessor.ModifyStatValue(statToModify, -this);
                }

                removeModifierCallback?.Invoke(this);     
            }  
        }

        public static TemporaryStatModifier GetTemporaryStatModifierInstance(
            StatType statType,
            ValueModifierType valueModifierType,
            float value,
            TemporaryStatModifierType temporaryStatModifierType,
            int duration)
        {
            return new TemporaryStatModifier(statType, valueModifierType, value, temporaryStatModifierType, duration);
        }

        public override object Clone()
        {
            return  new TemporaryStatModifier(StatType, ValueModifierType, Value, TemporaryStatModifierType, Duration);
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
