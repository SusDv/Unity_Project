using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

namespace BattleModule.Utility
{
    [Serializable]
    public class BattleCameraHelper : MonoBehaviour
    {
        [field: SerializeField] 
        public Camera MainCamera { get; private set; }
        
        [field: SerializeField]
        public Transform LookTarget { get; private set; }

        [field: SerializeField] 
        private List<CameraInfo> _cameras = new ();

        public Dictionary<ActionType, CinemachineVirtualCamera> BattleCameras =>
            _cameras.ToDictionary(c => c.Type, c => c.CinemachineVirtualCamera);
        

        [Serializable]
        public class CameraInfo
        {
            [field: SerializeField]
            public ActionType Type { get; private set; }

            [field: SerializeField]
            public CinemachineVirtualCamera CinemachineVirtualCamera { get; private set; }
        }
    }
}