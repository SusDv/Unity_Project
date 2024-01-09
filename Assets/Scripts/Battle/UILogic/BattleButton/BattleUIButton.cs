using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BattleModule.UI.Button
{
    public class BattleUIButton : MonoBehaviour
    {
        public event Action OnButtonClick;

        public void OnPointerClick(BaseEventData baseEventData) 
        {
            if ((baseEventData as PointerEventData).button == PointerEventData.InputButton.Left) 
            {
                OnButtonClick?.Invoke();
            }
        }

        private void OnDestroy()
        {
            OnButtonClick = null;
        }
    }
}
