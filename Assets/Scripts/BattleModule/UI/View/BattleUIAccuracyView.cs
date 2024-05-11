using TMPro;
using UnityEngine;

namespace BattleModule.UI.View
{
    public class BattleUIAccuracyView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _accuracyText;

        public void SetData(string accuracy)
        {
            _accuracyText.text = $"{accuracy}%";
        }
    }
}