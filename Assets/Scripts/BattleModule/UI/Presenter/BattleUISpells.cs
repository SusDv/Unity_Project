using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BattleModule.Controllers;
using BattleModule.UI.View;
using BattleModule.Actions.BattleActions;
using BattleModule.UI.BattleButton;

namespace BattleModule.UI.Presenter
{
    public class BattleUISpells : MonoBehaviour
    {
        [Header("Panel")] 
        [SerializeField] 
        private GameObject _battleUISpellsPanel;
        
        [Header("Parent")]
        [SerializeField]
        private GameObject _battleUISpellsParent;

        [Header("View")]
        [SerializeField]
        private BattleUISpellView _battleUISpellView;

        [Header("Button")]
        [SerializeField]
        private BattleUIDefaultButton _battleUIDefaultButton;
        
        private List<BattleUISpellView> _battleUISpells;

        private BattleActionController _battleActionController;

        private Character _characterToHaveTurn;

        public void Init(BattleActionController battleActionController,
            BattleTurnController battleTurnController) 
        {
            _battleUISpells = new List<BattleUISpellView>();

            _battleActionController = battleActionController;

            _battleUIDefaultButton.OnButtonClick += OnSpellsButtonClick;

            battleTurnController.OnCharacterToHaveTurnChanged += OnCharacterToHaveTurnChanged;
        }

        private void OnCharacterToHaveTurnChanged(List<Character> charactersToHaveTurn)
        {
            _characterToHaveTurn = charactersToHaveTurn.First();

            DisplayCharacterSpells(_characterToHaveTurn);
        }

        private void OnSpellsButtonClick(object o)
        {
            _battleUISpellsPanel.SetActive(!_battleUISpellsPanel.activeSelf);
        }

        private void BattleSpellsClear()
        {
            _battleUISpells = new List<BattleUISpellView>();

            for (var i = 0; i < _battleUISpellsParent.transform.childCount; i++)
            {
                Destroy(_battleUISpellsParent.transform.GetChild(i).gameObject);
            }
        }

        private void DisplayCharacterSpells(Character characterToHaveTurn) 
        {
            BattleSpellsClear();

            foreach (var spell in characterToHaveTurn.GetCharacterSpells().GetSpells()) 
            {
                var battleUISpellView = Instantiate(_battleUISpellView,
                    _battleUISpellsParent.transform.position,
                    Quaternion.identity,
                    _battleUISpellsParent.transform);

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
