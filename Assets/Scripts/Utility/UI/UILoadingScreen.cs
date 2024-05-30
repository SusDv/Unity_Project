using UnityEngine;

namespace Utility.UI
{
    public class UILoadingScreen : MonoBehaviour
    {
        public void SetVisibility(bool visibility)
        {
            GetComponent<Canvas>().enabled = visibility;
        }
    }
}
