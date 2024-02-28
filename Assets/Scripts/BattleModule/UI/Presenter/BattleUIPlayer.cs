using BattleModule.Controllers;
using BattleModule.UI.View;
using StatModule.Utility.Enums;
using System.Collections.Generic;
using System.Linq;
using BattleModule.UI.Presenter.SceneSettings.Player;
using CharacterModule;
using CharacterModule.Stats.Base;
using UnityEngine;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIPlayer : MonoBehaviour
    {
        private BattlePlayerSceneSettings _battlePlayerSceneSettings;

        private List<Character> _players;

        private List<BattleUIPlayerView> _battleUIPlayers;
        
        [Inject]
        private void Init(BattlePlayerSceneSettings battlePlayerSceneSettings, BattleSpawner battleSpawner)
        {
            _battlePlayerSceneSettings = battlePlayerSceneSettings;
            
            _battleUIPlayers = new List<BattleUIPlayerView>();

            _players = battleSpawner.GetSpawnedCharacters().Where((character) => (character is Player)).ToList();
            
            CreateBattleUICharacters();
        }

        private void CreateBattleUICharacters() 
        {
            foreach (var character in _players)
            {
                var battleUICharacterView = Instantiate(
                    _battlePlayerSceneSettings.BattleUIPlayerView,
                    _battlePlayerSceneSettings.BattleUIPlayersPanel.transform.position,
                    Quaternion.identity, _battlePlayerSceneSettings.BattleUIPlayersPanel.transform);

                _battleUIPlayers.Add(battleUICharacterView);

                battleUICharacterView.SetData(character.GetCharacterInformation(), character.GetCharacterStats());
            }
        }
    }
}
