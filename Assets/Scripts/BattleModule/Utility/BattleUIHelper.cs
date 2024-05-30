using System;
using UnityEngine;

namespace BattleModule.Utility
{
    [Serializable]
    public class BattleUIHelper : MonoBehaviour
    {
        [field: SerializeField]
        public Canvas DynamicCanvas { get; private set; }
        
        [field: SerializeField]
        public Canvas WorldCanvas { get; private set; }

        [field: SerializeField] 
        public Camera MainCamera { get; private set; }
    }
}