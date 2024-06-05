using System.Linq;
using System.Collections.Generic;
using BattleModule.Actions.Types;
using BattleModule.UI.View;
using BattleModule.Controllers.Modules;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.UI.Presenter.SceneReferences.Spells;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using Utility;
using Utility.Constants;
using UnityEngine;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUISpells : MonoBehaviour, ILoadingUnit
    {
        [SerializeField]
        private BattleSpellsSceneReference _battleSpellsSceneReference;

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
            
            _battleSpellsSceneReference.BattleSpellsMenuButton.OnButtonClick += OnSpellsButtonClick;

            _battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;

            return UniTask.CompletedTask;
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            _characterInAction = battleTurnContext.CharactersInTurn.First();

            DisplayCharacterSpells();
        }

        private void OnSpellsButtonClick()
        {
            _battleSpellsSceneReference.BattleUISpellsPanel.SetActive(!_battleSpellsSceneReference.BattleUISpellsPanel.activeSelf);
        }

        private void BattleSpellsClear()
        {
            _battleUISpells = new List<BattleUISpellView>();

            for (var i = 0; i < _battleSpellsSceneReference.BattleUISpellsParent.transform.childCount; i++)
            {
                Destroy(_battleSpellsSceneReference.BattleUISpellsParent.transform.GetChild(i).gameObject);
            }
        }

        private void DisplayCharacterSpells() 
        {
            BattleSpellsClear();

            foreach (var spell in _characterInAction.SpellContainer.GetSpells()) 
            {
                var battleUISpellView = Instantiate(_battleUISpellView,
                    _battleSpellsSceneReference.BattleUISpellsParent.transform.position,
                    Quaternion.identity,
                    _battleSpellsSceneReference.BattleUISpellsParent.transform);

                battleUISpellView.SetData(spell.ObjectInformation.Icon);

                battleUISpellView.OnButtonClick += OnSpellClick;

                _battleUISpells.Add(battleUISpellView);
            }
        }

        private void OnSpellClick(BattleUISpellView clickedSpell) 
        {
            var selectedSpell = _characterInAction.SpellContainer.GetSpells()[_battleUISpells.IndexOf(clickedSpell)];

            _battleActionController.SetBattleAction<SpellAction>(selectedSpell);
        }
    }
}
