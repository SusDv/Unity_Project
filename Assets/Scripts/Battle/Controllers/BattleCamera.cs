using Cinemachine;
using UnityEngine;

namespace BattleModule.Controllers 
{
    public class BattleCamera
    {
        private CinemachineVirtualCamera _battleCamera;   

        public BattleCamera(CinemachineVirtualCamera battleCamera)
        {
            _battleCamera = battleCamera;
        }

        public void SetBattleCameraTarget(Transform targetTransform)
        {
            _battleCamera.Follow = targetTransform;
        }
    }
}