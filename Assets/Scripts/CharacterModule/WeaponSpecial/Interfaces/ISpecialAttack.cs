using Utility.ObserverPattern;

namespace CharacterModule.WeaponSpecial.Interfaces
{
    public interface ISpecialAttack : ISubject
    {
        public void Charge(float amount);

        public bool IsReady();
    }
}