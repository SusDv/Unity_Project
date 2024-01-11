using System;
using UnityEngine;
using StatModule.Interfaces;
using StatModule.Modifier.ValueModifier;
using StatModule.Utility.Enums;

namespace StatModule.Modifier
{
    [Serializable]
    public class TemporaryStatModifier : BaseStatModifier 
    {
        private TemporaryStatModifier(
            StatType statType,
            ValueModifierType valueModifierType,
            float value,
            bool appliedOnce,
            bool appliedEveryTurn,
            bool appliedEveryCycle,
            int duration) : base(statType, valueModifierType, value) 
        {
            AppliedOnce = appliedOnce;
            AppliedEveryTurn = appliedEveryTurn;               
            AppliedEveryCycle = appliedEveryCycle;
            Duration = duration;
        }

        [field: SerializeField]
        public bool AppliedOnce { get; private set; } = false;

        [field: SerializeField]
        public bool AppliedEveryTurn { get; private set; } = false;

        [field: SerializeField]
        public bool AppliedEveryCycle { get; private set; } = false;

        [field: SerializeField]
        public int Duration { get; set; } = -1;

        private bool _modified;

        public override void Modify(IStat statToModify, Action<BaseStatModifier> addModifierCallback, Action<BaseStatModifier> removeModifierCallback) 
        {
            Duration = _modified ? Duration - 1 : Duration;

            if (!_modified)
            {
                if (AppliedOnce)
                {
                    ValueModifierProcessor.ModifyStatValue(statToModify, this);
                }

                _modified = true;
                addModifierCallback?.Invoke(this);
            }
            else 
            {
                if(AppliedEveryTurn || AppliedEveryCycle) 
                {
                    ValueModifierProcessor.ModifyStatValue(statToModify, this);
                }
            }

            if (Duration <= 0) 
            {
                if (AppliedOnce) 
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
            bool appliedOnce,
            bool appliedEveryTurn,
            bool appliedEveryCycle,
            int duration)
        {
            return new TemporaryStatModifier(statType, valueModifierType, value, appliedOnce, appliedEveryTurn, appliedEveryCycle, duration);
        }

        public override object Clone()
        {
            return  new TemporaryStatModifier(StatType, ValueModifierType, Value, AppliedOnce, AppliedEveryTurn, AppliedEveryCycle, Duration);
        }
    }
}
