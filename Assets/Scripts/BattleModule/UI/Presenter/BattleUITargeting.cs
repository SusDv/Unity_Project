using System.Collections.Generic;
using System.Linq;
using BattleModule.Controllers;
using BattleModule.UI.Presenter.SceneSettings.Targeting;
using CharacterModule;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUITargeting : MonoBehaviour
    {
        private BattleTargetingSceneSettings _battleTargetingSceneSettings;

        private readonly List<RectTransform> _battleTargetGroups = new ();

        private readonly List<Image> _battleTargetImages = new ();
        
        [Inject]
        private void Init(BattleTargetingSceneSettings battleTargetingSceneSettings,
            BattleTargetingController battleTargetingController,
            BattleSpawner battleSpawner)
        {
            _battleTargetingSceneSettings = battleTargetingSceneSettings;
            
            battleSpawner.OnCharactersSpawned += OnCharactersSpawned;
            
            battleTargetingController.OnCharacterTargetChanged += BattleCharacterTarget;
        }

        private void OnCharactersSpawned(List<Character> spawnedCharacters)
        {
            CreateBattleTargets(spawnedCharacters.Count(s => s is Enemy));
        }

        private void CreateBattleTargets(int spawnedCharactersCount)
        {
            for (var i = 0; i < spawnedCharactersCount; i++) 
            {
                var targetGroup = Instantiate(_battleTargetingSceneSettings.TargetGroupPrefab,
                    _battleTargetingSceneSettings.BattleTargetingCanvas.transform);

                var targetImage = Instantiate(_battleTargetingSceneSettings.CharacterTargetImage,
                    targetGroup.transform);
                
                _battleTargetGroups.Add(targetGroup);
                
                _battleTargetImages.Add(targetImage);
            }
        }

        private Vector2 GetLocalPosition(Vector3 position)
        {
            Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(_battleTargetingSceneSettings.MainCamera, position);
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_battleTargetingSceneSettings.BattleTargetingCanvas.transform as RectTransform, screenPosition, _battleTargetingSceneSettings.MainCamera, out var localPosition);

            return localPosition;
        }

        private void HideActiveBattleTargets()
        {
            foreach (var targetingImage in _battleTargetImages.Where(obj => obj.gameObject.activeSelf))
            {
                targetingImage.gameObject.SetActive(false);
            }
        }

        private void BattleCharacterTarget(List<Character> charactersTargeted)
        {
            HideActiveBattleTargets();

            int numberOfTargets = charactersTargeted.Count;
            
            foreach (var characterTargeted in charactersTargeted)
            {
                SetupTargetingGroup(--numberOfTargets, charactersTargeted.IndexOf(characterTargeted), GetLocalPosition(characterTargeted.transform.position));
            }
        }

        private void SetupTargetingGroup(int imageIndex, int characterIndex, Vector3 position)
        {
            var targetingGroup = _battleTargetGroups[characterIndex];

            targetingGroup.anchoredPosition = position;
            
            SetupTargetImage(imageIndex, targetingGroup);
        }

        private void SetupTargetImage(int index, Transform targetingGroup)
        {
            var targetingImage = _battleTargetImages[index];
            
            targetingImage.transform.SetParent(targetingGroup);
            
            targetingImage.transform.LookAt(_battleTargetingSceneSettings.MainCamera.transform.position);

            targetingImage.gameObject.SetActive(true);
        }
    }
}
