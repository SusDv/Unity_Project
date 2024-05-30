using BattleModule.Actions.BattleActions.Outcome;
using TMPro;
using UnityEngine;

namespace BattleModule.UI.View
{
    public class BattleUIOutcomeView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _outcomeText;
        
        public void SetData(BattleActionOutcome battleActionOutcome)
        {
            _outcomeText.text = battleActionOutcome.SubIntervalType.ToString();
        }
    }
}