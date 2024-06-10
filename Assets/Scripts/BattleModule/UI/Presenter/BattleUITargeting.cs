using System.Collections.Generic;
using System.Linq;
using BattleModule.Controllers.Modules;
using BattleModule.UI.View;
using BattleModule.Utility;
using CharacterModule.Types;
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
        
        private readonly List<BattleUITargetView> _battleTargetGroups = new ();

        private readonly List<RectTransform> _battleTargetImages = new ();
        
        [Inject]
        private void Init(AssetLoader assetLoader, BattleUIHelper battleUIHelper,
            BattleTargetingController battleTargetingController)
        {
            _assetLoader = assetLoader;

            _battleUIHelper = battleUIHelper;
            
            _battleTargetingController = battleTargetingController;
        }
        private void CreateTargetGroups(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _battleTargetGroups.Add(
                    Instantiate(_targetGroupPrefab,
                        Vector3.zero,
                        Quaternion.identity,
                        _battleUIHelper.WorldCanvas.transform));
            }
        }

        private void CreateTargetImagePool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _battleTargetImages.Add(
                    Instantiate(_targetImagePrefab, 
                    _battleUIHelper.WorldCanvas.transform));
            }
        }

        private void ResetActiveTargets()
        {
            foreach (var targetGroup in _battleTargetGroups)
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
                SetupTargetingGroup(charactersTargeted.IndexOf(character), character.SizeHelper.GetCharacterCenter(-0.5f));
            }
        }

        private void SetupTargetingGroup(int characterIndex, Vector3 position)
        {
            var targetingGroup = _battleTargetGroups[characterIndex];

            targetingGroup.transform.position = position;
            
            targetingGroup.AddTarget(GetTargetImage());
        }

        private int GetMaxCharacterType(IEnumerable<Character> characters)
        {
            return characters
                .GroupBy(c => c.GetType())
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .Where(x => x.Type == typeof(Player) || x.Type == typeof(Enemy))
                .Max(x => x.Count);
        }

        public UniTask Load(List<Character> spawnedCharacters)
        {
            _targetGroupPrefab = _assetLoader.GetLoadedAsset<BattleUITargetView>(RuntimeConstants.AssetsName.TargetGroupView);

            _targetImagePrefab = _assetLoader.GetLoadedAsset<RectTransform>(RuntimeConstants.AssetsName.TargetImageView);
            
            _battleTargetingController.OnTargetsChanged += BattleTargets;

            int maxCharacterType = GetMaxCharacterType(spawnedCharacters);
            
            CreateTargetGroups(maxCharacterType);
            
            CreateTargetImagePool(maxCharacterType);
            
            return UniTask.CompletedTask;
        }
    }
}
