using TMPro;
using UnityEngine;

namespace Utility.UI
{
    public class UILoadingScreen : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _progressText;
        
        
        public void SetVisibility(bool visibility)
        {
            GetComponent<Canvas>().enabled = visibility;
        }
    }
}
