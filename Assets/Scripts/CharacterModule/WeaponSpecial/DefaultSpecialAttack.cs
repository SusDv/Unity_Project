using System.Collections.Generic;
using CharacterModule.WeaponSpecial.Interfaces;
using UnityEngine;
using Utility.ObserverPattern;

namespace CharacterModule.WeaponSpecial
{
    public class DefaultSpecialAttack : ISpecialAttack
    {
        private readonly float _maxEnergy;

        private float _currentEnergyAmount;

        private readonly List<IObserver> _observers;

        public DefaultSpecialAttack(float maxEnergy)
        {
            _maxEnergy = maxEnergy;

            _observers = new List<IObserver>();
        }

        public void Charge(float amount)
        {
            _currentEnergyAmount = Mathf.Clamp(_currentEnergyAmount + amount, 0, _maxEnergy);
            
            NotifyObservers(amount < 0);
        }

        public bool IsReady()
        {
            if (Mathf.RoundToInt(_maxEnergy - _currentEnergyAmount) != 0)
            {
                return false;
            }

            _currentEnergyAmount = 0;
            
            NotifyObservers(false);

            return true;
        }
        
        private float GetChargeRatio()
        {
            return _currentEnergyAmount / _maxEnergy;
        }

        private void NotifyObservers(bool negativeChange)
        {
            _observers.ForEach(o => o.UpdateValue(GetChargeRatio(), negativeChange));
        }

        public void AttachObserver(IObserver observer)
        {
            _observers.Add(observer);
            
            observer.UpdateValue(GetChargeRatio(), false);
        }

        public void DetachObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}