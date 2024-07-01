using System;
using UnityEngine;

 namespace CharacterModule.Stats.Base 
{
    [Serializable]
    public class Stat
    {
        protected Stat(
            float baseValue, 
            float baseValueScaleFactor,
            float currentValue,
            float maxValue) 
        {
            BaseValue = baseValue;
            
            BaseValueScaleFactor = baseValueScaleFactor;
            
            _currentValue = maxValue;
            
            _maxValue = currentValue;
        }
        
        private float _currentValue;
        
        private float _maxValue;
        
        [field: SerializeField]
        public float BaseValue { get; set; }

        [field: SerializeField]
        public float BaseValueScaleFactor { get; set; }

        public virtual float CurrentValue
        {
            get => _currentValue;
            
            set => _currentValue = _maxValue = Mathf.Clamp(value, 0f, value);
        }

        public virtual float MaxValue
        {
            get => _maxValue;

            set => _currentValue = _maxValue = Mathf.Clamp(value, 0f, value);
        }

        private void InitClone(bool initClone)
        {
            if (!initClone)
            {
                return;
            }
            
            MaxValue = BaseValue;

            CurrentValue = MaxValue;
        }

        public Stat Clone(bool isCapped, bool initClone = false)
        {
            InitClone(initClone);

            if (isCapped)
            {
                return new MaxStat(BaseValue, BaseValueScaleFactor, CurrentValue, MaxValue);
            }
            
            return new Stat(BaseValue, BaseValueScaleFactor, CurrentValue, MaxValue);
        }
    }
    
    public class MaxStat : Stat
    {
        private float _currentValue;
        
        private float _maxValue;

        public MaxStat(float baseValue, float baseValueScaleFactor, float currentValue, float maxValue)
            : base(baseValue, baseValueScaleFactor, currentValue, maxValue)
        {
            _currentValue = currentValue;
            
            _maxValue = maxValue;
        }

        public override float MaxValue
        {
            get => _maxValue;
            
            set
            {
                _currentValue = value * CalculateFinalValuePercentage();
                
                _maxValue = Mathf.Clamp(value, 0f, value);
            }
        }

        public override float CurrentValue
        {
            get => _currentValue; 
            
            set => _currentValue = Mathf.Clamp(value, 0f, _maxValue);
        }

        private float CalculateFinalValuePercentage()
        {
            return _maxValue == 0 ? 0 : _currentValue / _maxValue;
        }
    }
}