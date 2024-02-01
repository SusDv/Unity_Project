using System.Collections.Generic;
using System.Linq;
using BattleModule.Controllers;
using BattleModule.UI.View;
using CharacterModule.Stats.Base;
using StatModule.Utility.Enums;
using UnityEngine;

namespace BattleModule.UI.Presenter
{
    public class BattleUIEnemy : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private BattleUIEnemyView _battleUIEnemyView;

        private List<Character> _enemyCharacters;

        private List<BattleUIEnemyView> _battleUIEnemies;

        public void Init()
        {
            _battleUIEnemies = new List<BattleUIEnemyView>();

            _enemyCharacters = BattleSpawner.Instance.GetSpawnedCharacters().Where((character) => character is Enemy).ToList();

            CreateBattleUIEnemies();
        }

        private void CreateBattleUIEnemies()
        {
            foreach (Character character in _enemyCharacters)
            {
                BattleUIEnemyView battleUICharacterView = Instantiate(
                    _battleUIEnemyView, character.gameObject.transform);

                character.GetCharacterStats().OnStatsModified += UpdateEnemyStats;

                _battleUIEnemies.Add(battleUICharacterView);

                UpdateEnemyStats(character.GetCharacterStats());
            }

        }
        private void UpdateEnemyStats(Stats stats)
        {
            int characterToUpdateIndex = _enemyCharacters.IndexOf(
                _enemyCharacters.Where((character) =>
                    character.GetCharacterStats().Equals(stats)).First());

            _battleUIEnemies[characterToUpdateIndex].SetData(
                stats.GetStatFinalValuesRatio(StatType.HEALTH, StatType.MAX_HEALTH));
        }

    }
}
