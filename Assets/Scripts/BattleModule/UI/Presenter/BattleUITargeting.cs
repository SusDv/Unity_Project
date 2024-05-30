using System.Collections.Generic;
using System.Linq;
using BattleModule.Controllers.Modules;
using BattleModule.Utility;
using CharacterModule.Types;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Utility.Constants;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUITargeting : MonoBehaviour, ILoadingUnit<List<Character>>
    {
        private AssetLoader _assetLoader;

        private BattleUIHelper _battleUIHelper;
        
        private BattleTargetingController _battleTargetingController;
        
        private RectTransform _targetGroupPrefab;
        
        private Image _targetImagePrefab;
        
        private readonly List<RectTransform> _battleTargetGroups = new ();

        private readonly List<Image> _battleTargetImages = new ();
        
        [Inject]
        private void Init(AssetLoader assetLoader, BattleUIHelper battleUIHelper,
            BattleTargetingController battleTargetingController)
        {
            _assetLoader = assetLoader;

            _battleUIHelper = battleUIHelper;
            
            _battleTargetingController = battleTargetingController;
        }

        public UniTask Load(List<Character> spawnedCharacters)
        {
            _targetGroupPrefab = _assetLoader.GetLoadedAsset<RectTransform>(RuntimeConstants.AssetsName.TargetGroupView);

            _targetImagePrefab = _assetLoader.GetLoadedAsset<Image>(RuntimeConstants.AssetsName.TargetImageView);
            
            _battleTargetingController.OnTargetsChanged += BattleTargets;
            
            CreateBattleTargets(spawnedCharacters.Count(s => s is Enemy));
            
            return UniTask.CompletedTask;
        }

        private void CreateBattleTargets(int spawnedCharactersCount)
        {
            for (var i = 0; i < spawnedCharactersCount; i++) 
            {
                var targetGroup = Instantiate(_targetGroupPrefab,
                    _battleUIHelper.WorldCanvas.transform);

                var targetImage = Instantiate(_targetImagePrefab, targetGroup.transform);
                
                _battleTargetGroups.Add(targetGroup);
                
                _battleTargetImages.Add(targetImage);
            }
        }

        private Vector2 GetLocalPosition(Vector3 position)
        {
            Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(_battleUIHelper.MainCamera, position);
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_battleUIHelper.WorldCanvas.transform as RectTransform, screenPosition, _battleUIHelper.MainCamera, out var localPosition);

            return localPosition;
        }

        private void HideActiveTargets()
        {
            foreach (var targetingImage in _battleTargetImages.Where(obj => obj.gameObject.activeSelf))
            {
                targetingImage.gameObject.SetActive(false);
            }
        }

        private void BattleTargets(List<Character> charactersTargeted)
        {
            HideActiveTargets();

            int numberOfTargets = charactersTargeted.Count;
            
            foreach (var characterTargeted in charactersTargeted)
            {
                SetupTargetingGroup(--numberOfTargets, charactersTargeted.IndexOf(characterTargeted), GetLocalPosition(characterTargeted.transform.position));
            }
        }

        private void SetupTargetingGroup(int imageIndex, int characterIndex, Vector3 position)
        {
            var targetingGroup = _battleTargetGroups[characterIndex];

            targetingGroup.anchoredPosition = position + Vector3.up * 0.6f;
            
            SetupTargetImage(imageIndex, targetingGroup);
        }

        private void SetupTargetImage(int index, Transform targetingGroup)
        {
            var targetingImage = _battleTargetImages[index];
            
            targetingImage.transform.SetParent(targetingGroup);
            
            targetingImage.transform.LookAt(_battleUIHelper.MainCamera.transform.position);

            targetingImage.gameObject.SetActive(true);
        }
    }
}
