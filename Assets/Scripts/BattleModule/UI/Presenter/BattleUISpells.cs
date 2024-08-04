using System.Linq;
using System.Collections.Generic;
using BattleModule.Actions.Types;
using BattleModule.UI.View;
using BattleModule.Controllers.Modules;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.UI.Presenter.SceneReferences.Spells;
using BattleModule.Utility.Interfaces;
using CharacterModule.Spells.Core;
using Cysharp.Threading.Tasks;
using Utility;
using Utility.Constants;
using UnityEngine;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUISpells : MonoBehaviour, ILoadingUnit, IUIElement, IBattleCancelable
    {
        [SerializeField]
        private BattleSpellsSceneReference _battleSpellsSceneReference;

        private BattleTurnController _battleTurnController;
        
        private BattleActionController _battleActionController;

        private BattleUIController _battleUIController;

        private BattleCancelableController _battleCancelableController;
        
        private AssetProvider _assetProvider;
        
        private List<BattleUISpellView> _battleUISpells;
        
        private BattleUISpellView _battleUISpellView;
        
        private List<SpellBase> _currentSpells;

        [Inject]
        private void Init(AssetProvider assetProvider,
            BattleActionController battleActionController,
            BattleUIController battleUIController,
            BattleTurnController battleTurnController,
            BattleCancelableController battleCancelableController)
        {
            _assetProvider = assetProvider;
            
            _battleActionController = battleActionController;

            _battleTurnController = battleTurnController;

            _battleUIController = battleUIController;

            _battleCancelableController = battleCancelableController;
        }

        public UniTask Load()
        {
            _battleUISpellView = _assetProvider.GetAssetByName<BattleUISpellView>(RuntimeConstants.AssetsName.SpellView);
            
            _battleSpellsSceneReference.BattleSpellsMenuButton.OnButtonClick += SpellsButtonClick;

            _battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;

            _battleUIController.AddAsUIElement(this);
            
            return UniTask.CompletedTask;
        }

        public void ToggleVisibility()
        {
            _battleSpellsSceneReference.BattleSpellsMenuButton.ToggleVisibility();
            
            _battleSpellsSceneReference.BattleUISpellsPanel.SetActive(false);
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            _currentSpells = battleTurnContext.CharactersInTurn.First().SpellsController.GetSpells();

            DisplayCharacterSpells();
        }

        private void SpellsButtonClick()
        {
            _battleSpellsSceneReference.BattleUISpellsPanel.SetActive(!_battleSpellsSceneReference.BattleUISpellsPanel.activeSelf);

            _battleCancelableController.TryAppendCancelable(this);
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

            foreach (var spell in _currentSpells) 
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
            SetupBattleAction(clickedSpell);

            SpellsButtonClick();
        }

        private void SetupBattleAction(BattleUISpellView clickedSpell)
        {
            _battleActionController.SetBattleAction<SpellAction>(GetSelectedSpell(clickedSpell));
        }

        private SpellBase GetSelectedSpell(BattleUISpellView clickedSpell)
        {
            return _currentSpells[_battleUISpells.IndexOf(clickedSpell)];
        }

        public bool TryCancel()
        {
            if (!_battleSpellsSceneReference.BattleUISpellsPanel.activeSelf)
            {
                return false;
            }
            _battleSpellsSceneReference.BattleUISpellsPanel.SetActive(false);
            
            return true;
        }
    }
}
