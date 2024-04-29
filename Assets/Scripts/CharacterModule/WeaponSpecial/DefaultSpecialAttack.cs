using System;
using System.Collections.Generic;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.Interfaces;
using CharacterModule.WeaponSpecial.Interfaces;
using UnityEngine;

namespace CharacterModule.WeaponSpecial
{
    public class DefaultSpecialAttack : ISpecialAttack
    {
        private readonly float _maxEnergy;

        private readonly List<IModifier> _targetModifiers;
        
        public float CurrentEnergyAmount { get; private set; }
        
        public event Action<float> OnEnergyChanged = delegate { };

        public DefaultSpecialAttack(float maxEnergy,
            List<IModifier> targetModifiers)
        {
            _maxEnergy = maxEnergy;

            _targetModifiers = targetModifiers;
        }

        public void Attack(List<Character> targets)
        {
            if (IsFullyCharged())
            {
                return;
            }

            CurrentEnergyAmount = 0;

            OnEnergyChanged?.Invoke(GetChargeRatio());
            
            ApplyTargetModifiers(targets);
        }

        public void Charge(float amount)
        {
            CurrentEnergyAmount = Mathf.Clamp(CurrentEnergyAmount + amount, 0, _maxEnergy);
            
            OnEnergyChanged?.Invoke(GetChargeRatio());
        }

        private bool IsFullyCharged()
        {
            return Mathf.RoundToInt(_maxEnergy - CurrentEnergyAmount) == 0;
        }

        private void ApplyTargetModifiers(List<Character> targets)
        {
            foreach (var target in targets)
            {
                var targetStats = target.CharacterStats;

                foreach (var modifier in _targetModifiers)
                {
                    targetStats.StatModifierManager.AddModifier(modifier);
                }
            }
        }

        private float GetChargeRatio()
        {
            return CurrentEnergyAmount / _maxEnergy;
        }
    }
}