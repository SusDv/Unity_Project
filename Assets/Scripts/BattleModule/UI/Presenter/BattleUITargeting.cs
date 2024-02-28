using System.Collections.Generic;
using BattleModule.Controllers;
using BattleModule.UI.Presenter.SceneSettings.Targeting;
using CharacterModule;
using UnityEngine;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUITargeting : MonoBehaviour
    {
        private BattleTargetingSceneSettings _battleTargetingSceneSettings;
        
        [Inject]
        private void Init(BattleTargetingSceneSettings battleTargetingSceneSettings,
            BattleTargetingController battleTargetingController)
        {
            _battleTargetingSceneSettings = battleTargetingSceneSettings;
            
            battleTargetingController.OnCharacterTargetChanged += BattleCharacterTarget;
        }

        private void ClearBattleCanvas()
        {
            for (var i = 0; i < _battleTargetingSceneSettings.BattleTargetingCanvas.transform.childCount; i++)
            {
                Destroy(_battleTargetingSceneSettings.BattleTargetingCanvas.transform.GetChild(i).gameObject);
            }
        }

        private void BattleCharacterTarget(List<Character> charactersTargeted)
        {
            ClearBattleCanvas();
            
            foreach (var characterTargeted in charactersTargeted)
            {
                var screenPosition =
                    RectTransformUtility.WorldToScreenPoint(_battleTargetingSceneSettings.MainCamera, characterTargeted.transform.position); 
                
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_battleTargetingSceneSettings.BattleTargetingCanvas.transform as RectTransform, screenPosition, _battleTargetingSceneSettings.MainCamera, out var localPosition);

                BattleSetUpTargetImage(localPosition, _battleTargetingSceneSettings.MainCamera.transform.position, characterTargeted == charactersTargeted[0]);
            }
        }

        private void BattleSetUpTargetImage(Vector3 anchoredPosition, Vector3 lookAt, bool isMain)
        {
            var targetingImage = Instantiate(_battleTargetingSceneSettings.CharacterTargetImage,
                _battleTargetingSceneSettings.BattleTargetingCanvas.transform);
            
            targetingImage.rectTransform.anchoredPosition = anchoredPosition;

            targetingImage.transform.localScale = isMain ? targetingImage.transform.localScale * 1.2f : targetingImage.transform.localScale * 0.8f;

            targetingImage.transform.LookAt(lookAt);
        }
    }
}
