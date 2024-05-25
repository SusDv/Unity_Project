using System.Collections.Generic;
using System.Linq;
using BattleModule.UI.View;
using BattleModule.UI.Presenter.SceneSettings.Player;
using BattleModule.Utility;
using CharacterModule.Types;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility.Constants;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIPlayer : MonoBehaviour, ILoadingUnit<List<Character>>
    {
        [SerializeField]
        private BattlePlayerSceneSettings _battlePlayerSceneSettings;

        private AssetLoader _assetLoader;
        
        private List<BattleUIPlayerView> _battleUIPlayers = new();
        
        private BattleUIPlayerView _battleUIPlayerView;
        
        [Inject]
        private void Init(AssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }
        
        public UniTask Load(List<Character> characters)
        {
            _battleUIPlayerView = _assetLoader.GetLoadedAsset<BattleUIPlayerView>(RuntimeConstants.AssetsName.PlayerView);
            
            CreateBattleUICharacters(characters.Where((character) => (character is Player)));

            return UniTask.CompletedTask;
        }
        
        private void CreateBattleUICharacters(IEnumerable<Character> players) 
        {
            foreach (var player in players)
            {
                var battleUICharacterView = Instantiate(
                    _battleUIPlayerView,
                    _battlePlayerSceneSettings.BattleUIPlayersPanel.transform.position,
                    Quaternion.identity, _battlePlayerSceneSettings.BattleUIPlayersPanel.transform);
                
                battleUICharacterView.SetData(player.EquipmentController.WeaponController.GetSpecialAttack(), player.BaseInformation, player.Stats);
                
                _battleUIPlayers.Add(battleUICharacterView);
            }
        }
    }
}
