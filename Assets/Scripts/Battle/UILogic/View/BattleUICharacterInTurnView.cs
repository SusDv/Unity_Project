using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.View 
{
    public class BattleUICharacterInTurnView : MonoBehaviour
    {
        [SerializeField] private Image _characterInTurnBorder; 

        [SerializeField] private TextMeshProUGUI _characterName;
        [SerializeField] private TextMeshProUGUI _characterBattlePoints;

        public void SetData(string characterName, string characterBattlePoints, bool isCharacterInTurn)
        {
            _characterName.text = characterName;
            _characterBattlePoints.text = $"BP: {characterBattlePoints}";
            _characterInTurnBorder.color = isCharacterInTurn ? Color.red : Color.black;
        }
    }
}

