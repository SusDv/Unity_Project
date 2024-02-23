using BattleModule.UI.BattleButton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.View 
{
    public class BattleUIItemView : BattleUIButton<BattleUIItemView>
    {
        [SerializeField] private Image _battleItemImage;
        [SerializeField] private TextMeshProUGUI _battleItemCount;

        public void SetData(Sprite battleItemImage, string battleItemCount)
        {
            _battleItemImage.sprite = battleItemImage;

            _battleItemCount.text = battleItemCount;
        }
    }
}