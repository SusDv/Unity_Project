using TMPro;
using UnityEngine;

namespace BattleModule.UI.View
{
    public class BattleUIActionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _battleActionText;

        public void SetData(string battleActionText) 
        {
            _battleActionText.text = battleActionText;
        }
    }
}