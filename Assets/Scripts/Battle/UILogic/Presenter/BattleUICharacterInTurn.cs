using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BattleModule.UI.View;

namespace BattleModule.UI.Presenter 
{
    public class BattleUICharacterInTurn : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private GameObject _battleCharacterInTurnPanel;

        [Header("View")]
        [SerializeField] private BattleUICharacterInTurnView _battleUICharacterInTurnView;

        public void InitCharactersInTurn(ref Action<List<Character>> characterInTurnChanged, List<Character> charactersInTurn)
        {
            characterInTurnChanged += BattleCharacterInTurnUpdate;

            BattleCharacterInTurnUpdate(charactersInTurn);
        }

        private void BattleCharacterInTurnClear()
        {
            for (int i = 0; i < _battleCharacterInTurnPanel.transform.childCount; i++)
            {
                Destroy(_battleCharacterInTurnPanel.transform.GetChild(i).gameObject);
            }
        }

        private void BattleCharacterInTurnUpdate(List<Character> charactersInTurn)
        {
            BattleCharacterInTurnClear();

            foreach (Character character in charactersInTurn)
            {
                BattleUICharacterInTurnView battleUICharacterInTurn = Instantiate(_battleUICharacterInTurnView,
                    _battleCharacterInTurnPanel.transform.position, Quaternion.identity,
                    _battleCharacterInTurnPanel.transform);

                battleUICharacterInTurn.SetData(character.gameObject.name, character.GetStats().GetStatFinalValue(StatModule.Utility.Enums.StatType.BATTLE_POINTS).ToString());
            }
        }
    }
}