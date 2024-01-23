#nullable enable
using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T? _instance;

        public static T Instance 
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                }

                return _instance;
            } 
        }

        private void OnDisable()
        {
            _instance = null;
        }
    }
}
