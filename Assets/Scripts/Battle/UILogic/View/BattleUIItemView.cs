using BattleModule.UI.Button;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.View 
{
    public class BattleUIItemView : BattleUIButton<BattleUIItemView>
    {
        [SerializeField] private Image _battleItemBorder;
        [SerializeField] private Image _battleItemImage;
        [SerializeField] private TextMeshProUGUI _battleItemCount;

        public void SetData(Sprite battleItemImage, string battleItemCount)
        {
            _battleItemImage.sprite = battleItemImage;

            _battleItemCount.text = battleItemCount;
        }
    }
}