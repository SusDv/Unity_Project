using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BattleModule.Accuracy;
using BattleModule.Controllers.Modules;
using BattleModule.UI.Presenter.SceneReferences.Accuracy;
using BattleModule.UI.View;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using Utility.Constants;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIAccuracy : MonoBehaviour, ILoadingUnit<List<Character>>
    {
        [SerializeField]
        private BattleAccuracySceneReference _battleAccuracySceneReference;
        
        private AssetProvider _assetProvider;
        
        private BattleAccuracyController _battleAccuracyController;

        private BattleTargetingController _battleTargetingController;
        
        private BattleUIAccuracyView _accuracyViewPrefab;
        
        private Dictionary<Character, BattleAccuracy> _battleAccuracies = new ();
        
        private readonly Dictionary<Character, BattleUIAccuracyView> _accuracyViews = new ();
        
        
        [Inject]
        private void Init(AssetProvider assetProvider,
            BattleAccuracyController battleAccuracyController,
            BattleTargetingController battleTargetingController)
        {
            _assetProvider = assetProvider;
            
            _battleAccuracyController = battleAccuracyController;

            _battleTargetingController = battleTargetingController;
        }

        private void HideActiveUI()
        {
            foreach (var accuracyView in _accuracyViews.Values.Where(a => a.gameObject.activeSelf))
            {
                accuracyView.gameObject.SetActive(false);
            }
        }
        
        private void OnTargetsChanged(List<Character> targets)
        {
            HideActiveUI();
            
            foreach (var target in targets.Distinct().ToList())
            {
                if (!_accuracyViews.TryGetValue(target, out var accuracyView))
                {
                    continue;
                }

                accuracyView.SetData(_battleAccuracies[target].HitRate.ToString(CultureInfo.CurrentCulture));
                
                accuracyView.gameObject.SetActive(true);
            }
        }
        
        private void OnAccuraciesChanged(Dictionary<Character, BattleAccuracy> accuracies)
        {
            _battleAccuracies = accuracies;
        }

        private void CreateAccuracyView(List<Character> characters)
        {
            foreach (var character in characters)
            {
                var accuracyView = Instantiate(
                    _accuracyViewPrefab,
                    character.SizeHelper.GetCharacterTop(), 
                    Quaternion.identity, 
                    _battleAccuracySceneReference.Parent);
                
                accuracyView.gameObject.SetActive(false);
                
                _accuracyViews.Add(character, accuracyView);
            }
        }

        public UniTask Load(List<Character> characters)
        {
            _accuracyViewPrefab = _assetProvider.GetAssetByName<BattleUIAccuracyView>(RuntimeConstants.AssetsName.AccuracyView);
            
            _battleAccuracyController.OnAccuraciesChanged += OnAccuraciesChanged;

            _battleTargetingController.OnTargetsChanged += OnTargetsChanged;
            
            CreateAccuracyView(characters);

            return UniTask.CompletedTask;
        }
    }
}