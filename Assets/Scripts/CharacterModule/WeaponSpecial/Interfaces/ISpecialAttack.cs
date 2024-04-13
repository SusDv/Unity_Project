using System.Collections.Generic;
using BattleModule.Utility.Interfaces;
using CharacterModule.CharacterType.Base;

namespace CharacterModule.WeaponSpecial.Interfaces
{
    public interface ISpecialAttack : ITargetableObject
    {
        public float EnergyToAttack { get; set; }

        public void Attack(List<Character> targets);

        public void Charge(float amount);
    }
}