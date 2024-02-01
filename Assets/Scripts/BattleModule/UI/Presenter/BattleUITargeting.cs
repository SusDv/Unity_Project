using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Init(ref Action<List<Character>> targetChangedAction) 
        {
            targetChangedAction += BattleCharacterTarget;
        }

        private void ClearBattleCanvas()
        {
            for (var i = 0; i < _battleTargetingCanvas.transform.childCount; i++)
            {
                Destroy(_battleTargetingCanvas.transform.GetChild(i).gameObject);
            }
        }

        private void BattleCharacterTarget(List<Character> charactersTargeted)
        {
            ClearBattleCanvas();
            
            foreach (var screenPosition in charactersTargeted.Select(targetedCharacter => RectTransformUtility.WorldToScreenPoint(_mainCamera, targetedCharacter.transform.position)))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_battleTargetingCanvas.transform as RectTransform, screenPosition, _mainCamera, out var localPosition);

                BattleSetUpTargetImage(localPosition, _mainCamera.transform.position);
            }
        }

        private void BattleSetUpTargetImage(Vector3 anchoredPosition, Vector3 lookAt)
        {
            var targetingImage = Instantiate(_characterTargetImage,
                _battleTargetingCanvas.transform);
            
            targetingImage.rectTransform.anchoredPosition = anchoredPosition;

            targetingImage.transform.LookAt(lookAt);

            targetingImage.gameObject.SetActive(true);
        }
    }
}
