using CharacterModule.WeaponSpecial.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.View
{
    public class BattleUISpecialAttackView : MonoBehaviour
    {
        [SerializeField] private Image _sliderImage;
        
        public void SetData(ISpecialAttack specialAttack)
        {
            specialAttack.OnEnergyChanged += UpdateValue;
        }

        private void UpdateValue(float updatedValue)
        {
            _sliderImage.fillAmount = updatedValue;
        }
    }
}