using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using StatModule.Utility.Enums;
using CharacterModule.Stats.Base;
using BattleModule.Controllers;
using BattleModule.UI.Presenter.SceneSettings.Enemy;
using BattleModule.UI.View;
using CharacterModule;

namespace BattleModule.UI.Presenter
{
    public class BattleUIEnemy : MonoBehaviour
    {
        private BattleEnemySceneSettings _battleEnemySceneSettings;
        
        private List<Character> _enemyCharacters;

        private List<BattleUIEnemyView> _battleUIEnemies;
        
        [Inject]
        private void Init(BattleEnemySceneSettings battleEnemySceneSettings,
            BattleSpawner battleSpawner)
        {
            _battleEnemySceneSettings = battleEnemySceneSettings;
            
            _battleUIEnemies = new List<BattleUIEnemyView>();

            _enemyCharacters = battleSpawner.GetSpawnedCharacters().Where((character) => character is Enemy).ToList();

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
