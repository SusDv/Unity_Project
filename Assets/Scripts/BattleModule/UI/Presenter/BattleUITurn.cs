using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using BattleModule.UI.View;
using BattleModule.Controllers;
using BattleModule.Utility.Interfaces;

namespace BattleModule.UI.Presenter 
{
    public class BattleUITurn : MonoBehaviour, ICharacterInTurnObserver
    {
        [Header("Panel")]
        [SerializeField] private GameObject _battleTurnParent;

        [Header("View")]
        [SerializeField] private BattleUITurnView _battleUITurnView;

        public void Init(BattleTurnController battleTurnController)
        {
            battleTurnController.AddCharacterInTurnObserver(this);
        }

        private void ClearTurnPanel()
        {
            for (int i = 0; i < _battleTurnParent.transform.childCount; i++)
            {
                Destroy(_battleTurnParent.transform.GetChild(i).gameObject);
            }
        }

        public void Notify(List<Character> charactersToHaveTurn)
        {
            ClearTurnPanel();

            foreach (Character character in charactersToHaveTurn)
            {
                BattleUITurnView battleUITurn = Instantiate(_battleUITurnView,
                      _battleTurnParent.transform.position, Quaternion.identity,
                      _battleTurnParent.transform);

                battleUITurn.SetData(character.gameObject.name, character.GetCharacterStats().GetStatFinalValue(StatModule.Utility.Enums.StatType.BATTLE_POINTS).ToString(), charactersToHaveTurn.First().name == character.name);
            }
        }
    }
}