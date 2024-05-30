using System;

namespace CharacterModule.WeaponSpecial.Interfaces
{
    public interface ISpecialAttack
    {
        public event Action<float> OnEnergyChanged;

        public void Charge(float amount);

        public bool IsReady();
    }
}