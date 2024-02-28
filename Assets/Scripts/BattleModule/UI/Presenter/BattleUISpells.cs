using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BattleModule.Controllers;
using BattleModule.UI.View;
using BattleModule.Actions.BattleActions;
using BattleModule.UI.Presenter.SceneSettings.Spells;
using CharacterModule;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUISpells : MonoBehaviour
    {
        private BattleSpellsSceneSettings _battleSpellsSceneSettings;
        
        private List<BattleUISpellView> _battleUISpells;

        private BattleActionController _battleActionController;

        private Character _characterToHaveTurn;

        [Inject]
        private void Init(BattleSpellsSceneSettings battleSpellsSceneSettings, 
            BattleActionController battleActionController,
            BattleTurnController battleTurnController)
        {
            _battleSpellsSceneSettings = battleSpellsSceneSettings;
            
            _battleUISpells = new List<BattleUISpellView>();

            _battleActionController = battleActionController;

            _battleSpellsSceneSettings.BattleSpellsMenuButton.OnButtonClick += OnSpellsButtonClick;

            battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;
        }

        private void OnCharactersInTurnChanged(List<Character> charactersToHaveTurn)
        {
            _characterToHaveTurn = charactersToHaveTurn.First();

            DisplayCharacterSpells(_characterToHaveTurn);
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

        private void DisplayCharacterSpells(Character characterToHaveTurn) 
        {
            BattleSpellsClear();

            foreach (var spell in characterToHaveTurn.GetCharacterSpells().GetSpells()) 
            {
                var battleUISpellView = Instantiate(_battleSpellsSceneSettings.BattleUISpellView,
                    _battleSpellsSceneSettings.BattleUISpellsParent.transform.position,
                    Quaternion.identity,
                    _battleSpellsSceneSettings.BattleUISpellsParent.transform);

                battleUISpellView.SetData(spell.SpellImage);

                battleUISpellView.OnButtonClick += OnSpellClick;

                _battleUISpells.Add(battleUISpellView);
            }
        }

        private void OnSpellClick(BattleUISpellView clickedSpell) 
        {
            var selectedSpell = _characterToHaveTurn.GetCharacterSpells().GetSpells()[_battleUISpells.IndexOf(clickedSpell)];

            _battleActionController.SetBattleAction<BattleSpellAction>(selectedSpell);
        }
    }
}
