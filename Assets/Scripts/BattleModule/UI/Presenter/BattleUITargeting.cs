using System;
using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.Presenter
{
    public class BattleUITargeting : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Canvas _battleTargetingCanvas;
        [SerializeField] private Image _characterTargetImage;

        [Header("Main Camera Reference")]
        [SerializeField] private Camera _mainCamera;

        public void Init(ref Action<Vector3> targetChangedAction) 
        {
            targetChangedAction += BattleCharacterTarget;
        }

        private void BattleCharacterTarget(Vector3 characterTargetedPosition)
        {
            if (characterTargetedPosition == Vector3.zero) 
            {
                _characterTargetImage.gameObject.SetActive(false);
                return;
            }

            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(_mainCamera, characterTargetedPosition);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_battleTargetingCanvas.transform as RectTransform, screenPosition, _mainCamera, out Vector2 localPosition);

            BattleSetUpTargetImage(localPosition, _mainCamera.transform.position);
        }

        private void BattleSetUpTargetImage(Vector3 anchoredPosition, Vector3 lookAt) 
        {
            _characterTargetImage.rectTransform.anchoredPosition = anchoredPosition;

            _characterTargetImage.transform.LookAt(lookAt);

            _characterTargetImage.gameObject.SetActive(true);
        }
    }
}
