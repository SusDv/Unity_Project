using System;
using Cinemachine;
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

        [field: SerializeField]
        public CinemachineVirtualCamera PlayerAttackCamera { get; private set; }
        
        [field: SerializeField]
        public CinemachineVirtualCamera ActionOnPlayerCamera { get; private set; }
    }
}