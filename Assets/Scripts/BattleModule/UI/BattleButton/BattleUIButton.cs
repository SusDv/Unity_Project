using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BattleModule.UI.BattleButton
{
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

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void OnApplicationQuit()
        {
            OnButtonClick = null;

            OnButtonOver = null;
        }
    }
}
