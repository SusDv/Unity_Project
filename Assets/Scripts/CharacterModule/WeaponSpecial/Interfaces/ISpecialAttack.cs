using System;
using System.Collections.Generic;
using CharacterModule.CharacterType.Base;

namespace CharacterModule.WeaponSpecial.Interfaces
{
    public interface ISpecialAttack
    {
        public float CurrentEnergyAmount { get; }
        
        public event Action<float> OnEnergyChanged;
        
        public void Attack(List<Character> targets);

        public void Charge(float amount);
    }
}