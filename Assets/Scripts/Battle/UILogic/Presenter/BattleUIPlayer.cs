using BattleModule.UI.View;
using StatModule.Core;
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

        private List<Character> _playerCharacters;

        private List<BattleUIPlayerView> _battleUIPlayers;

        public void InitBattleUICharacter(List<Character> playerCharacters) 
        {
            _battleUIPlayers = new List<BattleUIPlayerView>();

            _playerCharacters = new List<Character>(playerCharacters
                .Where((character) => character is Player));

            CreateBattleUICharacters();
        }

        private void CreateBattleUICharacters() 
        {  
            foreach (Character character in _playerCharacters) 
            {
                BattleUIPlayerView battleUICharacterView = Instantiate(
                    _battleUIPlayerView, _battleUIPlayersPanel.transform.position,
                    Quaternion.identity, _battleUIPlayersPanel.transform);

                character.GetStats().OnStatsModified += UpdatePlayerPanels;

                _battleUIPlayers.Add(battleUICharacterView);

                UpdatePlayerPanels(character.GetStats());
            }
            
        }
        private void UpdatePlayerPanels(Stats stats)
        {
            int characterToUpdateIndex = _playerCharacters.IndexOf(
                _playerCharacters.Where((character) =>
                    character.GetStats().Equals(stats)).First());

            _battleUIPlayers[characterToUpdateIndex].SetData(
                    null,
                    stats.GetStatFinalValuesRatio(StatType.HEALTH, StatType.MAX_HEALTH),
                    stats.GetStatFinalValuesRatio(StatType.MANA, StatType.MAX_MANA));
        }
    }
}
