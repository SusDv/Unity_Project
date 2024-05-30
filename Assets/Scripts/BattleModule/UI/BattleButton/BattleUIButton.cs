using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BattleModule.UI.BattleButton
{
    public class BattleUIButton : MonoBehaviour
    {
        public event Action OnButtonClick;

        public event Action OnButtonOver;
        

        public void OnPointerClick(BaseEventData baseEventData) 
        {
            if (((PointerEventData) baseEventData).button == PointerEventData.InputButton.Left) 
            {
                OnButtonClick?.Invoke();
            }
        }

        public void OnPointerOver(BaseEventData baseEventData) 
        {
            OnButtonOver?.Invoke();
        }
    }

    public abstract class BattleUIButton<T> : MonoBehaviour
        where T : class
    {
        public event Action<T> OnButtonClick;

        public event Action<T> OnButtonOver;

        public void OnPointerClick(BaseEventData baseEventData) 
        {
            if (((PointerEventData) baseEventData).button == PointerEventData.InputButton.Left) 
            {
                OnButtonClick?.Invoke(this as T);
            }
        }

        public void OnPointerOver(BaseEventData baseEventData) 
        {
            OnButtonOver?.Invoke(this as T);
        }
    }
}
