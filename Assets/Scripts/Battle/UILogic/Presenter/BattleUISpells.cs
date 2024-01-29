using BattleModule.ActionCore;
using BattleModule.Controllers;
using BattleModule.UI.View;
using SpellModule.Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleModule.UI.Presenter
{
    public class BattleUISpells : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField]
        private GameObject _battleUISpellsPanel;

        [Header("View")]
        [SerializeField]
        private BattleUISpellView _battleUISpellView;

        private List<BattleUISpellView> _battleUISpells;

        private BattleActionController _battleActionController;

        private Character _characterToHaveTurn;

        public void Init(BattleActionController battleActionController,
            BattleTurnController battleTurnController) 
        {
            _battleUISpells = new List<BattleUISpellView>();

            _battleActionController = battleActionController;

            battleTurnController.OnCharacterToHaveTurnChanged += OnCharacterToHaveTurnChanged;
        }

        private void OnCharacterToHaveTurnChanged(List<Character> charactersToHaveTurn)
        {
            _characterToHaveTurn = charactersToHaveTurn.First();

            DisplayCharacterSpells(_characterToHaveTurn);
        }

        private void BattleSpellsClear()
        {
            _battleUISpells = new List<BattleUISpellView>();

            for (int i = 0; i < _battleUISpellsPanel.transform.childCount; i++)
            {
                Destroy(_battleUISpellsPanel.transform.GetChild(i).gameObject);
            }
        }

        private void DisplayCharacterSpells(Character characterToHaveTurn) 
        {
            BattleSpellsClear();

            foreach (SpellBase spell in characterToHaveTurn.GetCharacterSpells().GetSpells()) 
            {
                BattleUISpellView battleUISpellView = Instantiate(_battleUISpellView,
                    _battleUISpellsPanel.transform.position,
                    Quaternion.identity,
                    _battleUISpellsPanel.transform);

                battleUISpellView.SetData(spell.SpellImage);

                battleUISpellView.OnButtonClick += OnSpellClick;

                _battleUISpells.Add(battleUISpellView);
            }
        }

        private void OnSpellClick(BattleUISpellView clickedSpell) 
        {
            SpellBase selectedSpell = _characterToHaveTurn.GetCharacterSpells().GetSpells()[_battleUISpells.IndexOf(clickedSpell)];

            _battleActionController.SetBattleAction<BattleSpellAction>(selectedSpell);
        }
    }
}
