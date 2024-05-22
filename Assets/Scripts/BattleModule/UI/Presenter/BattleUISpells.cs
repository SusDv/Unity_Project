using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Types;
using UnityEngine;
using BattleModule.UI.View;
using BattleModule.Controllers.Modules;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.UI.Presenter.SceneSettings.Spells;
using BattleModule.Utility;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using Utility;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUISpells : MonoBehaviour, ILoadingUnit
    {
        [SerializeField]
        private BattleSpellsSceneSettings _battleSpellsSceneSettings;

        private BattleTurnController _battleTurnController;
        
        private BattleActionController _battleActionController;
        
        private AssetLoader _assetLoader;
        
        private List<BattleUISpellView> _battleUISpells;
        
        private BattleUISpellView _battleUISpellView;
        
        private Character _characterInAction;

        [Inject]
        private void Init(AssetLoader assetLoader,
            BattleActionController battleActionController,
            BattleTurnController battleTurnController)
        {
            _assetLoader = assetLoader;
            
            _battleActionController = battleActionController;

            _battleTurnController = battleTurnController;
        }

        public UniTask Load()
        {
            _battleUISpellView = _assetLoader.GetLoadedAsset<BattleUISpellView>(RuntimeConstants.AssetsName.SpellView);
            
            _battleSpellsSceneSettings.BattleSpellsMenuButton.OnButtonClick += OnSpellsButtonClick;

            _battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;

            return UniTask.CompletedTask;
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            _characterInAction = battleTurnContext.CharacterInAction;

            DisplayCharacterSpells();
        }

        private void OnSpellsButtonClick(object o)
        {
            _battleSpellsSceneSettings.BattleUISpellsPanel.SetActive(!_battleSpellsSceneSettings.BattleUISpellsPanel.activeSelf);
        }

        private void BattleSpellsClear()
        {
            _battleUISpells = new List<BattleUISpellView>();

            for (var i = 0; i < _battleSpellsSceneSettings.BattleUISpellsParent.transform.childCount; i++)
            {
                Destroy(_battleSpellsSceneSettings.BattleUISpellsParent.transform.GetChild(i).gameObject);
            }
        }

        private void DisplayCharacterSpells() 
        {
            BattleSpellsClear();

            foreach (var spell in _characterInAction.CharacterSpells.GetSpells()) 
            {
                var battleUISpellView = Instantiate(_battleUISpellView,
                    _battleSpellsSceneSettings.BattleUISpellsParent.transform.position,
                    Quaternion.identity,
                    _battleSpellsSceneSettings.BattleUISpellsParent.transform);

                battleUISpellView.SetData(spell.ObjectInformation.Icon);

                battleUISpellView.OnButtonClick += OnSpellClick;

                _battleUISpells.Add(battleUISpellView);
            }
        }

        private void OnSpellClick(BattleUISpellView clickedSpell) 
        {
            var selectedSpell = _characterInAction.CharacterSpells.GetSpells()[_battleUISpells.IndexOf(clickedSpell)];

            _battleActionController.SetBattleAction<SpellAction>(selectedSpell);
        }
    }
}
