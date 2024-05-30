using System;
using CharacterModule.WeaponSpecial.Interfaces;
using UnityEngine;

namespace CharacterModule.WeaponSpecial
{
    public class DefaultSpecialAttack : ISpecialAttack
    {
        private readonly float _maxEnergy;
        
        public float CurrentEnergyAmount { get; private set; }
        
        public event Action<float> OnEnergyChanged = delegate { };

        public DefaultSpecialAttack(float maxEnergy)
        {
            _maxEnergy = maxEnergy;
        }

        public void Charge(float amount)
        {
            CurrentEnergyAmount = Mathf.Clamp(CurrentEnergyAmount + amount, 0, _maxEnergy);
            
            OnEnergyChanged?.Invoke(GetChargeRatio());
        }

        public bool IsReady()
        {
            if (Mathf.RoundToInt(_maxEnergy - CurrentEnergyAmount) != 0)
            {
                return false;
            }

            CurrentEnergyAmount = 0;
            
            OnEnergyChanged?.Invoke(GetChargeRatio());

            return true;
        }
        
        private float GetChargeRatio()
        {
            return CurrentEnergyAmount / _maxEnergy;
        }
    }
}