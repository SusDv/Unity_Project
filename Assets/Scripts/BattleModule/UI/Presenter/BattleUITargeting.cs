using System.Collections.Generic;
using System.Linq;
using BattleModule.Controllers.Modules;
using BattleModule.Utility;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
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
        
        private GameObject _targetGroupPrefab;
        
        private GameObject _targetImagePrefab;
        
        private readonly List<GameObject> _battleTargetGroups = new ();

        private readonly List<GameObject> _battleTargetImages = new ();
        
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
            _targetGroupPrefab = _assetLoader.GetLoadedAsset(RuntimeConstants.AssetsName.TargetGroupView);

            _targetImagePrefab = _assetLoader.GetLoadedAsset(RuntimeConstants.AssetsName.TargetImageView);
            
            _battleTargetingController.OnTargetsChanged += BattleTargets;
            
            CreateTargetGroups(spawnedCharacters.Count);
            
            CreateTargetImagePool(spawnedCharacters.Count);
            
            return UniTask.CompletedTask;
        }

        private void CreateTargetGroups(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _battleTargetGroups.Add(Instantiate(_targetGroupPrefab, _battleUIHelper.WorldCanvas.transform));
            }
        }

        private void CreateTargetImagePool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _battleTargetImages.Add(Instantiate(_targetImagePrefab));
            }
        }

        private void HideActiveTargets()
        {
            foreach (var targetingImage in _battleTargetImages.Where(obj => obj.gameObject.activeSelf))
            {
                targetingImage.transform.SetParent(_battleUIHelper.DynamicCanvas.transform);
                
                targetingImage.gameObject.SetActive(false);
            }
        }

        private void AssignTargetImage(Transform parent)
        {
            var targetingImage = _battleTargetImages.First(i => i.transform.parent == _battleUIHelper.DynamicCanvas.transform);
            
            targetingImage.transform.SetParent(parent);
            
            targetingImage.gameObject.SetActive(true);
        }

        private void BattleTargets(List<Character> charactersTargeted)
        {
            HideActiveTargets();
            
            foreach (var characterTargeted in charactersTargeted)
            {
                SetupTargetingGroup(charactersTargeted.IndexOf(characterTargeted), characterTargeted.SizeHelper.GetCharacterCenter());
            }
        }
        
        private Vector3 GetLocalPosition(Vector3 position)
        {
            Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(_battleUIHelper.MainCamera, position);
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_battleUIHelper.WorldCanvas.transform as RectTransform, screenPosition, _battleUIHelper.MainCamera, out var localPosition);

            return new Vector3(localPosition.x, localPosition.y, position.z);
        }

        private void SetupTargetingGroup(int characterIndex, Vector3 position)
        {
            var targetingGroup = _battleTargetGroups[characterIndex];

            targetingGroup.transform.position = position;
            
            AssignTargetImage(targetingGroup.transform);
        }
    }
}
