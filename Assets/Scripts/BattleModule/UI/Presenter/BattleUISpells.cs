using System.Collections.Generic;
using UnityEngine;
using BattleModule.UI.View;
using BattleModule.Actions.BattleActions;
using BattleModule.Actions.BattleActions.ActionTypes;
using BattleModule.Controllers.Modules;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.UI.Presenter.SceneSettings.Spells;
using CharacterModule;
using CharacterModule.CharacterType.Base;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUISpells : MonoBehaviour
    {
        private BattleSpellsSceneSettings _battleSpellsSceneSettings;
        
        private List<BattleUISpellView> _battleUISpells;

        private BattleActionController _battleActionController;

        private Character _characterInAction;

        [Inject]
        private void Init(BattleSpellsSceneSettings battleSpellsSceneSettings, 
            BattleActionController battleActionController,
            BattleTurnController battleTurnController)
        {
            _battleSpellsSceneSettings = battleSpellsSceneSettings;
            
            _battleActionController = battleActionController;

            _battleSpellsSceneSettings.BattleSpellsMenuButton.OnButtonClick += OnSpellsButtonClick;

            battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;
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
                var battleUISpellView = Instantiate(_battleSpellsSceneSettings.BattleUISpellView,
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
