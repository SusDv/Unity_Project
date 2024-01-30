using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.View 
{
    public class BattleUIPlayerView : MonoBehaviour
    {
        [SerializeField] private Image _characterImage;
        [SerializeField] private Image _healthBarImage;
        [SerializeField] private Image _manaBarImage;

        public void SetData(Sprite characterImage, float healthFillAmount, float manaFillAmount) 
        {
            _characterImage.sprite = characterImage;
            _healthBarImage.fillAmount = healthFillAmount;
            _manaBarImage.fillAmount = manaFillAmount;
        }
    }
}