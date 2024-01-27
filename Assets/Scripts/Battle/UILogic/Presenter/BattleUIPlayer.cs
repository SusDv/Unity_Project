using BattleModule.UI.View;
using StatModule.Base;
using StatModule.Utility.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleModule.UI.Presenter
{
    public class BattleUIPlayer : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private GameObject _battleUIPlayersPanel;

        [Header("View")]
        [SerializeField] private BattleUIPlayerView _battleUIPlayerView;

        private List<Player> _playerCharacters;

        private List<BattleUIPlayerView> _battleUIPlayers;

        public void InitBattleUICharacter() 
        {
            _battleUIPlayers = new List<BattleUIPlayerView>();

            _playerCharacters = FindObjectsByType<Player>(FindObjectsSortMode.None).ToList();

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
