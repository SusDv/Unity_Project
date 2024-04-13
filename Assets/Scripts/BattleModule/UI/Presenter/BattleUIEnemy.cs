using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using BattleModule.Controllers.Modules;
using BattleModule.UI.Presenter.SceneSettings.Enemy;
using BattleModule.UI.View;
using CharacterModule;
using CharacterModule.CharacterType.Base;

namespace BattleModule.UI.Presenter
{
    public class BattleUIEnemy : MonoBehaviour
    {
        private BattleEnemySceneSettings _battleEnemySceneSettings;
        
        private List<Character> _enemyCharacters;

        private List<BattleUIEnemyView> _battleUIEnemies = new();
        
        [Inject]
        private void Init(BattleEnemySceneSettings battleEnemySceneSettings,
            BattleSpawner battleSpawner)
        {
            _battleEnemySceneSettings = battleEnemySceneSettings;
            
            battleSpawner.OnCharactersSpawned += OnCharactersSpawned;
        }
        
        private void OnCharactersSpawned(List<Character> characters)
        {
            _enemyCharacters = characters.Where((character) => character is Enemy).ToList();
            
            CreateBattleUIEnemies();
        }

        private void CreateBattleUIEnemies()
        {
            foreach (var character in _enemyCharacters)
            {
                var battleUICharacterView = Instantiate(
                    _battleEnemySceneSettings.BattleUIEnemyView, character.gameObject.transform);
                
                _battleUIEnemies.Add(battleUICharacterView);

                battleUICharacterView.SetData(character.CharacterStats);
            }

        }
    }
}
