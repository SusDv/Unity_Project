using System;
using System.Collections.Generic;
using System.Linq;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Types.Base;
using CharacterModule.WeaponSpecial.Interfaces;
using UnityEngine;

namespace CharacterModule.WeaponSpecial
{
    public class DefaultSpecialAttack : ISpecialAttack
    {
        private readonly float _maxEnergy;

        private readonly (List<ITemporaryModifier> temporaryModifiers, List<IModifier> modifiers) _targetModifiers;
        
        public float CurrentEnergyAmount { get; private set; }
        
        public event Action<float> OnEnergyChanged = delegate { };

        public DefaultSpecialAttack(float maxEnergy,
            (List<ITemporaryModifier> temporaryModifiers, List<IModifier> modifiers) targetModifiers)
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
            foreach (var targetStats in targets.Select(target => target.CharacterStats))
            {
                foreach (var modifier in _targetModifiers.modifiers)
                {
                    targetStats.AddModifier(modifier);
                }
                
                foreach (var modifier in _targetModifiers.temporaryModifiers)
                {
                    targetStats.AddModifier(modifier);
                }
            }
        }

        private float GetChargeRatio()
        {
            return CurrentEnergyAmount / _maxEnergy;
        }
    }
}