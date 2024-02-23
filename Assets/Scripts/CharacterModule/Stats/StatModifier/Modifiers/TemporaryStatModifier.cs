using System;
using BattleModule.Actions;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using StatModule.Modifier.ValueModifier;
using StatModule.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class TemporaryStatModifier : BaseStatModifier 
    {
        private TemporaryStatModifier(StatType statType, 
            ValueModifierType valueModifierType, 
            ModifierCapType modifierCapType,
            float value,
            TemporaryStatModifierType temporaryStatModifierType,
            int duration) : base(statType, valueModifierType, modifierCapType, value) 
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

        private int AssignedOnTurn { get; set; }

        public override void Modify(Stat statToModify,
            Action<BaseStatModifier> addModifierCallback,
            Action<BaseStatModifier> removeModifierCallback) 
        {
            if (!Initialized)
            {
                AssignedOnTurn = BattleEventManager.Instance.GetCurrentTurn();
                
                if (TemporaryStatModifierType.Equals(TemporaryStatModifierType.APPLIED_ONCE))
                {
                    ValueModifierProcessor.ModifyStatValue(statToModify, this);
                }

                Initialized = true;
                
                addModifierCallback?.Invoke(this);
                
                return;
            }
            
            if (TemporaryStatModifierType != TemporaryStatModifierType.APPLIED_EVERY_CYCLE)
            {
                Duration--;
            }
            else
            {
                if (BattleEventManager.Instance.GetCurrentTurn() - AssignedOnTurn ==
                    BattleEventManager.Instance.GetMaximumTurnsInCycle())
                {
                    Duration--;
                    
                    ValueModifierProcessor.ModifyStatValue(statToModify, this);
                }
            }
                
            if(TemporaryStatModifierType.Equals(TemporaryStatModifierType.APPLIED_EVERY_TURN))
            {
                ValueModifierProcessor.ModifyStatValue(statToModify, this);
            }

            if (Duration != 0)
            {
                return;
            }
            
            if (TemporaryStatModifierType.Equals(TemporaryStatModifierType.APPLIED_ONCE))
            {
                ValueModifierProcessor.ModifyStatValue(statToModify, -this);
            }

            removeModifierCallback?.Invoke(this);
        }

        public static TemporaryStatModifier GetTemporaryStatModifierInstance(
            StatType statType, 
            ValueModifierType valueModifierType, 
            ModifierCapType modifierCapType,
            float value,
            TemporaryStatModifierType temporaryStatModifierType,
            int duration)
        {
            return new TemporaryStatModifier(statType, valueModifierType, modifierCapType, value, temporaryStatModifierType, duration);
        }

        public override object Clone()
        {
            return  new TemporaryStatModifier(StatType, ValueModifierType, ModifierCapType, Value, TemporaryStatModifierType, Duration);
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
