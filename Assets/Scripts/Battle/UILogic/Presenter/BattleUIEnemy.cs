using System.Collections.Generic;
using System.Linq;
using BattleModule.UI.View;
using StatModule.Base;
using StatModule.Utility.Enums;
using UnityEngine;

namespace BattleModule.UI.Presenter
{
    public class BattleUIEnemy : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private BattleUIEnemyView _battleUIEnemyView;

        private List<Enemy> _enemyCharacters;

        private List<BattleUIEnemyView> _battleUIEnemies;

        public void InitBattleUIEnemy()
        {
            _battleUIEnemies = new List<BattleUIEnemyView>();

            _enemyCharacters = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();

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
