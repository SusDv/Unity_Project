using System;
using System.Collections.Generic;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.WeaponSpecial.Interfaces;
using UnityEngine;

namespace CharacterModule.WeaponSpecial
{
    public class DefaultSpecialAttack : ISpecialAttack
    {
        private readonly float _maxEnergy;

        private readonly List<BaseStatModifier> _targetModifiers;
        
        public float CurrentEnergyAmount { get; private set; }
        
        public event Action<float> OnEnergyChanged;

        public DefaultSpecialAttack(float maxEnergy,
            List<BaseStatModifier> targetModifiers)
        {
            _maxEnergy = maxEnergy;

            _targetModifiers = targetModifiers;
        }

        public void Attack(List<Character> targets)
        {
            if (Mathf.RoundToInt(_maxEnergy - CurrentEnergyAmount) != 0)
            {
                return;
            }

            CurrentEnergyAmount = 0;

            foreach (var target in targets)
            {
                var targetStats = target.CharacterStats;

                foreach (var modifier in _targetModifiers)
                {
                    targetStats.ApplyStatModifier(modifier);
                }
            }
        }

        public void Charge(float amount)
        {
            CurrentEnergyAmount = Mathf.Clamp(CurrentEnergyAmount + amount, 0, _maxEnergy);
            
            OnEnergyChanged?.Invoke(CurrentEnergyAmount / _maxEnergy);
        }
    }
}