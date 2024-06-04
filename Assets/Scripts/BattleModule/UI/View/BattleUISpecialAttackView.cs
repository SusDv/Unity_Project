using UnityEngine;
using UnityEngine.UI;
using Utility.ObserverPattern;

namespace BattleModule.UI.View
{
    public class BattleUISpecialAttackView : MonoBehaviour, IObserver
    {
        [SerializeField] private Image _sliderImage;
        
        public void SetData(ISubject specialAttack)
        {
            specialAttack.AttachObserver(this);
        }

        public void UpdateValue(float updatedValue, bool negativeChange)
        {
            _sliderImage.fillAmount = updatedValue;
        }
    }
}