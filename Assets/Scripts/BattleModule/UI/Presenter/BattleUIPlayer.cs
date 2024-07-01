using System.Collections.Generic;
using System.Linq;
using BattleModule.UI.Presenter.SceneReferences.Player;
using BattleModule.UI.View;
using CharacterModule.Types;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using Utility.Constants;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIPlayer : MonoBehaviour, ILoadingUnit<List<Character>>
    {
        [SerializeField]
        private BattlePlayerSceneReference _battlePlayerSceneReference;

        private AssetProvider _assetProvider;
        
        private List<BattleUIPlayerView> _battleUIPlayers = new();
        
        private BattleUIPlayerView _battleUIPlayerView;
        
        [Inject]
        private void Init(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public UniTask Load(List<Character> characters)
        {
            _battleUIPlayerView = _assetProvider.GetAssetByName<BattleUIPlayerView>(RuntimeConstants.AssetsName.PlayerView);
            
            CreateBattleUICharacters(characters.Where((character) => (character is Player)));

            return UniTask.CompletedTask;
        }
        
        private void CreateBattleUICharacters(IEnumerable<Character> players) 
        {
            foreach (var player in players)
            {
                var battleUICharacterView = Instantiate(
                    _battleUIPlayerView,
                    _battlePlayerSceneReference.BattleUIPlayersPanel.transform.position,
                    Quaternion.identity, _battlePlayerSceneReference.BattleUIPlayersPanel.transform);
                
                battleUICharacterView.SetData(player.EquipmentController.WeaponController.GetSpecialAttack(), player.BaseInformation, player.Stats);
                
                _battleUIPlayers.Add(battleUICharacterView);
            }
        }
    }
}
