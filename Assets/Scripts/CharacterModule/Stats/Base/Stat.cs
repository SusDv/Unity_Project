using System;
using System.Collections.Generic;
using CharacterModule.Stats.Interfaces;
using StatModule.Utility;
using StatModule.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.Base 
{
    [Serializable]
    public class Stat : ICloneable
    {
        private Stat(
            StatType statType, 
            float baseValue, 
            float baseValueScaleFactor, 
            bool isCapped, 
            float finalValue,
            float maxValue) 
        {
            StatType = statType;
            BaseValue = baseValue;
            BaseValueScaleFactor = baseValueScaleFactor;
            IsCapped = isCapped;
            MaxValue = maxValue;
            FinalValue = finalValue;
        }
        
        private float _finalValue;
        
        private float _maxValue;

        private List<IStatObserver> _statObservers = new();

        [field: SerializeField]    
        public StatType StatType { get; set; }

        [field: SerializeField]
        public float BaseValue { get; set; }

        [field: SerializeField]
        public float BaseValueScaleFactor { get; set; }

        [field: SerializeField]
        public bool IsCapped { get; set; }

        public float FinalValue
        {
            get => _finalValue;
            set
            {
                if (!IsCapped)
                {
                    _finalValue = _maxValue = Mathf.Clamp(value, 0f, value);
                    
                    return;
                }
                
                _finalValue = Mathf.Clamp(value, 0f, _maxValue);
                
                _statObservers?.ForEach(o => o.UpdateValue(StatInfo.GetInstance(BaseValue, FinalValue, MaxValue)));
            }
        }

        public float MaxValue
        {
            get => _maxValue;
            set
            {
                if (!IsCapped)
                {
                    _maxValue = Mathf.Clamp(value, 0f, value);

                    return;
                }
                
                
                if (_maxValue != 0)
                {
                    float valuePercentage = _finalValue / _maxValue;
                    
                    _finalValue = value * valuePercentage;
                }
                
                _maxValue = Mathf.Clamp(value, 0f, value);

                _statObservers?.ForEach(o => o.UpdateValue(StatInfo.GetInstance(BaseValue, FinalValue, MaxValue)));
            }
        }
        
        public object Clone()
        {
            return new Stat(StatType, BaseValue, BaseValueScaleFactor, IsCapped, FinalValue, MaxValue);
        }

        public void AttachObserver(IStatObserver statObserver)
        {
            _statObservers.Add(statObserver);
        }

        public void DetachObserver(IStatObserver statObserver)
        {
            _statObservers.Remove(statObserver);
        }
    }
}