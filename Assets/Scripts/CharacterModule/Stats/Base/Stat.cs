using System;
using StatModule.Interfaces;
using StatModule.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.Base 
{
    [Serializable]
    public class Stat : IStat, ICloneable
    {
        private Stat(
            StatType statType, 
            float baseValue, 
            float baseValuePrescaler, 
            bool isMaxStat, 
            float finalValue) 
        {
            StatType = statType;
            BaseValue = baseValue;
            BaseValuePrescaler = baseValuePrescaler;
            IsMaxStat = isMaxStat;
            _finalValue = finalValue;
        }

        private float _finalValue;

        [field: SerializeField]    
        public StatType StatType { get; set; }

        [field: SerializeField]
        public float BaseValue { get; set; }

        [field: SerializeField]
        public float BaseValuePrescaler { get; set; }

        [field: SerializeField]
        public bool IsMaxStat { get; set; }

        public float FinalValue 
        { 
            get { return _finalValue; } 
            set 
            {
                if (DependencyWithStat != null)
                {
                    if (!IsMaxStat)
                    {
                        _finalValue = Mathf.Clamp(value, 0f, DependencyWithStat._finalValue);
                    }
                    else
                    {
                        float valuePercentage = _finalValue != 0 ?
                            DependencyWithStat._finalValue / _finalValue : 1;

                        _finalValue = value;

                        DependencyWithStat._finalValue = value * valuePercentage;
                    }
                }
                else 
                {
                    _finalValue = Mathf.Clamp(value, 0f , value);
                }
            } 
        }
        
        public Stat DependencyWithStat { get; set; }

        public object Clone()
        {
            return new Stat(StatType, BaseValue, BaseValuePrescaler, IsMaxStat, FinalValue);
        }
    }
}