using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BattleModule.Accuracy;
using BattleModule.Controllers.Modules;
using BattleModule.UI.Presenter.SceneSettings.Accuracy;
using BattleModule.UI.View;
using BattleModule.Utility;
using CharacterModule.CharacterType.Base;
using UnityEngine;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIAccuracy : MonoBehaviour, ILoadingUnit<List<Character>>
    {
        private BattleAccuracySceneSettings _sceneSettings;

        private BattleAccuracyController _battleAccuracyController;

        private BattleTargetingController _battleTargetingController;
        
        private Dictionary<Character, BattleAccuracy> _battleAccuracies = new ();
        
        private readonly Dictionary<Character, BattleUIAccuracyView> _accuracyViews = new ();
        
        [Inject]
        private void Init(BattleAccuracySceneSettings battleAccuracySceneSettings,
            BattleAccuracyController battleAccuracyController,
            BattleTargetingController battleTargetingController)
        {
            _sceneSettings = battleAccuracySceneSettings;

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
                if (_accuracyViews.TryGetValue(target, out var accuracyView))
                {
                    accuracyView.SetData(Mathf.RoundToInt(_battleAccuracies[target].HitRate * 100).ToString(CultureInfo.CurrentCulture));
                
                    accuracyView.gameObject.SetActive(true);
                }
            }
        }

        public Task Load(List<Character> characters)
        {
            _battleAccuracyController.OnAccuraciesChanged += OnAccuraciesChanged;

            _battleTargetingController.OnTargetsChanged += OnTargetsChanged;
            
            CreateAccuracyView(characters);

            return Task.CompletedTask;
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
                    _sceneSettings.AccuracyViewPrefab,
                    character.transform.position + Vector3.up * 1.6f, 
                    Quaternion.identity, 
                    _sceneSettings.Parent.transform);
                
                accuracyView.gameObject.SetActive(false);
                
                _accuracyViews.Add(character, accuracyView);
            }
        }
    }
}