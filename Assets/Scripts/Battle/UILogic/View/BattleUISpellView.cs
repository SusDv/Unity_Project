using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleModule.UI.View 
{
    public class BattleUISpellView : MonoBehaviour
    {
        [SerializeField] private Image _spellImage;

        public event Action<BattleUISpellView> OnSpellOver;

        public event Action<BattleUISpellView> OnSpellClick;

        public void SetData(Sprite spellImage) 
        {
            _spellImage.sprite = spellImage;
        }

        public void OnPointerClick(BaseEventData baseEventData)
        {
            if ((baseEventData as PointerEventData).button == PointerEventData.InputButton.Left)
            {
                OnSpellClick?.Invoke(this);
            }
        }
    }
}


