using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleModule.UI.View 
{
    public class BattleUIItemView : MonoBehaviour
    {
        [SerializeField] private Image _battleItemBorder;
        [SerializeField] private Image _battleItemImage;
        [SerializeField] private TextMeshProUGUI _battleItemCount;

        public event Action<BattleUIItemView> OnItemOver;
        public event Action<BattleUIItemView> OnItemClick;

        public void SetData(Sprite battleItemImage, string battleItemCount)
        {
            _battleItemImage.sprite = battleItemImage;

            _battleItemCount.text = battleItemCount;
        }

        public void OnPointerOver(BaseEventData baseEventData)
        {   
            OnItemOver?.Invoke(this);
        }

        public void OnPointerClick(BaseEventData baseEventData) 
        {
            if ((baseEventData as PointerEventData).button == PointerEventData.InputButton.Left)
            {
                OnItemClick?.Invoke(this);
            }
        }

        private void OnDestroy()
        {
            OnItemOver = null;

            OnItemClick = null;
        }
    }
}