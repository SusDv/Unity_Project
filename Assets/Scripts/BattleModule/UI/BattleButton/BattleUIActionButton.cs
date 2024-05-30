using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.BattleButton
{
    public class BattleUIActionButton : BattleUIButton
    {
        [SerializeField]
        private Image _buttonImage;
        
        public void SetButtonImage(Sprite sprite)
        {
            _buttonImage.sprite = sprite;
        }
    }
}