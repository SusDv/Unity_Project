using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BattleModule.Accuracy;
using BattleModule.Controllers.Modules;
using BattleModule.UI.Presenter.SceneSettings.Accuracy;
using BattleModule.UI.View;
using CharacterModule.CharacterType.Base;
using UnityEngine;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIAccuracy : MonoBehaviour
    {
        private BattleAccuracySceneSettings _sceneSettings;

        private List<Character> _spawnedCharacters = new ();
        
        private readonly Dictionary<Character, BattleUIAccuracyView> _accuracyViews = new ();

        private Dictionary<Character, BattleAccuracy> _battleAccuracies = new ();
        
        [Inject]
        private void Init(BattleAccuracySceneSettings battleAccuracySceneSettings,
            BattleAccuracyController battleAccuracyController,
            BattleTargetingController battleTargetingController,
            BattleSpawner battleSpawner)
        {
            _sceneSettings = battleAccuracySceneSettings;
            
            battleSpawner.OnCharactersSpawned += OnCharactersSpawned;
            
            battleTargetingController.OnTargetsChanged += OnTargetsChanged;
            
            battleAccuracyController.OnAccuraciesChanged += OnAccuraciesChanged;
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
                _accuracyViews[target].SetData(Mathf.RoundToInt(_battleAccuracies[target].HitRate * 100).ToString(CultureInfo.CurrentCulture));
                
                _accuracyViews[target].gameObject.SetActive(true);
            }
        }

        private void OnCharactersSpawned(List<Character> characters)
        {
            _spawnedCharacters = characters;
            
            CreateAccuracyView(_spawnedCharacters);
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