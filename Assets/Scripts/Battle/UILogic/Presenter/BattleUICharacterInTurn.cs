using System;
using System.Collections.Generic;
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

        private Action<List<Character>> _characterInTurnChanged = delegate { };

        public void InitCharactersInTurn(ref Action<List<Character>> characterInTurnChanged, List<Character> charactersInTurn)
        {
            _characterInTurnChanged = characterInTurnChanged;

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

            for(int i = 0; i < charactersInTurn.Count; i++) 
            {
                BattleUICharacterInTurnView battleUICharacterInTurn = Instantiate(_battleUICharacterInTurnView,
                    _battleCharacterInTurnPanel.transform.position, Quaternion.identity,
                    _battleCharacterInTurnPanel.transform);

                battleUICharacterInTurn.SetData(charactersInTurn[i].gameObject.name, charactersInTurn[i].GetCharacterStats().GetStatFinalValue(StatModule.Utility.Enums.StatType.BATTLE_POINTS).ToString(), (i == 0) ? true : false);
            }
        }

        private void OnDisable()
        {
            _characterInTurnChanged -= BattleCharacterInTurnUpdate;
        }
    }
}