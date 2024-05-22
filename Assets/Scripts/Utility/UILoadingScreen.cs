using UnityEngine;

namespace Utility
{
    public class UILoadingScreen : MonoBehaviour
    {
        public void SetVisibility(bool visibility)
        {
            GetComponent<Canvas>().enabled = visibility;
        }
    }
}
