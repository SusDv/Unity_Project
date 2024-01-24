using BattleModule.ActionCore;
using BattleModule.ActionCore.Context;
using BattleModule.ActionCore.Interfaces;
using BattleModule.UI.View;
using SpellModule.Base;
using System;
using System.Collections.Generic;
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

        private IBattleAction _battleAction;

        private List<BattleUISpellView> _battleUISpells = new List<BattleUISpellView>();

        private Character _characterInTurn;

        public void InitBattleUISpells(IBattleAction battleAction,
            ref Action<List<Character>> characterInTurnChanged,
            List<Character> charactersInTurn) 
        {
            _battleAction = battleAction;

            characterInTurnChanged += CreateUISpells;

            CreateUISpells(charactersInTurn);
        }
        private void BattleSpellsClear()
        {
            for (int i = 0; i < _battleUISpellsPanel.transform.childCount; i++)
            {
                Destroy(_battleUISpellsPanel.transform.GetChild(i).gameObject);
            }
        }

        private void CreateUISpells(List<Character> charactersInTurn) 
        {
            _characterInTurn = charactersInTurn[0];
            
            BattleSpellsClear();

            foreach (SpellBase spell in _characterInTurn.GetCharacterSpells().GetSpells()) 
            {
                BattleUISpellView battleUISpellView = Instantiate(_battleUISpellView,
                    _battleUISpellsPanel.transform.position,
                    Quaternion.identity,
                    _battleUISpellsPanel.transform);

                battleUISpellView.SetData(spell.SpellImage);

                battleUISpellView.OnSpellClick += OnSpellClick;

                _battleUISpells.Add(battleUISpellView);
            }
        }

        private void OnSpellClick(BattleUISpellView clickedSpell) 
        {
            SpellBase selectedSpell = _characterInTurn.GetCharacterSpells().GetSpells()[_battleUISpells.IndexOf(clickedSpell)];

            _battleAction.BattleAction =
                BattleSpellAction.GetBattleSpellActionInstance(
                    BattleActionContext.GetBattleActionContextInstance(
                        selectedSpell));
        }
    }
}
