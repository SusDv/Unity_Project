using TMPro;
using UnityEngine;

namespace BattleModule.UI.View 
{
    public class BattleUICharacterInTurnView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _characterName;
        [SerializeField] private TextMeshProUGUI _characterBattlePoints;

        public void SetData(string characterName, string characterBattlePoints)
        {
            _characterName.text = characterName;
            _characterBattlePoints.text = $"BP: {characterBattlePoints}";
        }
    }
}

