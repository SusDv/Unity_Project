using System.Collections.Generic;
using System.Linq;
using BattleModule.Controllers.Modules;
using BattleModule.UI.View;
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
        
        private BattleUITargetView _targetGroupPrefab;
        
        private RectTransform _targetImagePrefab;
        
        private readonly Dictionary<GameObject, BattleUITargetView> _battleTargetGroups = new ();

        private readonly List<RectTransform> _battleTargetImages = new ();
        
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
            _targetGroupPrefab = _assetLoader.GetLoadedAsset<BattleUITargetView>(RuntimeConstants.AssetsName.TargetGroupView);

            _targetImagePrefab = _assetLoader.GetLoadedAsset<RectTransform>(RuntimeConstants.AssetsName.TargetImageView);
            
            _battleTargetingController.OnTargetsChanged += BattleTargets;
            
            CreateTargetGroups(spawnedCharacters);
            
            CreateTargetImagePool(spawnedCharacters.Count);
            
            return UniTask.CompletedTask;
        }

        private void CreateTargetGroups(List<Character> characters)
        {
            foreach (var character in characters)
            {
                _battleTargetGroups.Add(character.gameObject,
                    Instantiate(_targetGroupPrefab, 
                        _battleUIHelper.WorldCanvas.transform));
            }
        }

        private void CreateTargetImagePool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _battleTargetImages.Add(Instantiate(_targetImagePrefab, 
                    _battleUIHelper.WorldCanvas.transform));
            }
        }

        private void ResetActiveTargets()
        {
            foreach (var targetGroup in _battleTargetGroups.Values)
            {
                targetGroup.ClearTargets(_battleUIHelper.WorldCanvas.transform);
            }
        }

        private RectTransform GetTargetImage()
        {
            return _battleTargetImages.First(i => i.transform.parent == _battleUIHelper.WorldCanvas.transform);
        }

        private void BattleTargets(List<Character> charactersTargeted)
        {
            ResetActiveTargets();

            foreach (var character in charactersTargeted)
            {
                SetupTargetingGroup(character.gameObject, character.SizeHelper.GetCharacterCenter(-0.5f));
            }
        }

        private void SetupTargetingGroup(GameObject character, Vector3 position)
        {
            var targetingGroup = _battleTargetGroups[character];

            targetingGroup.transform.position = position;
            
            targetingGroup.AddTarget(GetTargetImage());
        }
    }
}
