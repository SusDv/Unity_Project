using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using BattleModule.UI.View;
using BattleModule.Controllers;

namespace BattleModule.UI.Presenter 
{
    public class BattleUITurn : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] private GameObject _battleTurnParent;

        [Header("View")]
        [SerializeField] private BattleUITurnView _battleUITurnView;

        public void Init(BattleTurnController battleTurnController)
        {
            battleTurnController.OnCharacterToHaveTurnChanged += DisplayTurnPanel;
        }

        private void ClearTurnPanel()
        {
            for (int i = 0; i < _battleTurnParent.transform.childCount; i++)
            {
                Destroy(_battleTurnParent.transform.GetChild(i).gameObject);
            }
        }

        private void DisplayTurnPanel(List<Character> charactersToHaveTurn)
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