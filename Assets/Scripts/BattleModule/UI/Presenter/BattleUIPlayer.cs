using BattleModule.Controllers;
using BattleModule.UI.View;
using StatModule.Utility.Enums;
using System.Collections.Generic;
using System.Linq;
using CharacterModule.Stats.Base;
using UnityEngine;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIPlayer : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private GameObject _battleUIPlayersPanel;

        [Header("View")]
        [SerializeField] private BattleUIPlayerView _battleUIPlayerView;

        private List<Character> _playerCharacters;

        private List<BattleUIPlayerView> _battleUIPlayers;
        
        public void Init(BattleSpawner battleSpawner) 
        {
            _battleUIPlayers = new List<BattleUIPlayerView>();

            _playerCharacters = battleSpawner.GetSpawnedCharacters().Where((character) => character is Player).ToList();

            CreateBattleUICharacters();
        }

        private void CreateBattleUICharacters() 
        {  
            foreach (Character character in _playerCharacters) 
            {
                BattleUIPlayerView battleUICharacterView = Instantiate(
                    _battleUIPlayerView, _battleUIPlayersPanel.transform.position,
                    Quaternion.identity, _battleUIPlayersPanel.transform);

                character.GetCharacterStats().OnStatsModified += UpdatePlayerPanels;

                _battleUIPlayers.Add(battleUICharacterView);

                UpdatePlayerPanels(character.GetCharacterStats());
            }
            
        }
        private void UpdatePlayerPanels(Stats stats)
        {
            int characterToUpdateIndex = _playerCharacters.IndexOf(
                _playerCharacters.Where((character) =>
                    character.GetCharacterStats().Equals(stats)).First());

            _battleUIPlayers[characterToUpdateIndex].SetData(
                    null,
                    stats.GetStatFinalValuesRatio(StatType.HEALTH, StatType.MAX_HEALTH),
                    stats.GetStatFinalValuesRatio(StatType.MANA, StatType.MAX_MANA));
        }
    }
}
