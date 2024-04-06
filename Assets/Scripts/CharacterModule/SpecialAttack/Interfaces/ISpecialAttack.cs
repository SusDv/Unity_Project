using System.Collections.Generic;
using BattleModule.Utility.Interfaces;

namespace CharacterModule.SpecialAttack.Interfaces
{
    public interface ISpecialAttack : ITargetableObject
    {
        public float EnergyToAttack { get; set; }

        public void Attack(Character source, List<Character> targets);

        public void Charge(float amount);
    }
}