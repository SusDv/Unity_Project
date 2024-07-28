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
        public enum CameraType
        {
            PLAYER_DEFAULT_ACTION,
            PLAYER_SPELL_ACTION,
            PLAYER_ALLY_ACTION,
            PLAYER_ITEM_ACTION,
            ENEMY_ATTACK_ACTION
        }
        
        [field: SerializeField] 
        public Camera MainCamera { get; private set; }
        
        [field: SerializeField]
        public Transform LookTarget { get; private set; }

        [field: SerializeField] 
        private List<CameraInfo> _cameras = new ();

        public Dictionary<CameraType, CinemachineVirtualCamera> Cameras =>
            _cameras.ToDictionary(c => c.Type, c => c.CinemachineVirtualCamera);

        [Serializable]
        public class CameraInfo
        {
            [field: SerializeField]
            public CameraType Type { get; private set; }

            [field: SerializeField]
            public CinemachineVirtualCamera CinemachineVirtualCamera { get; private set; }
        }
    }
}