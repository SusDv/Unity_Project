using BattleModule.Actions.Context;
using BattleModule.Input;
using BattleModule.Utility;
using CharacterModule.Types.Base;
using Cinemachine;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.Controllers.Modules 
{
    public class BattleCamera : ILoadingUnit
    {
        private readonly BattleCameraHelper _battleCameraHelper;

        private readonly BattleActionController _battleActionController;

        private readonly BattleInput _battleInput;

        private CinemachineVirtualCamera _currentCamera;
        
        [Inject]
        public BattleCamera(BattleCameraHelper battleCameraHelper,
            BattleActionController battleActionController,
            BattleTargetingController battleTargetingController,
            BattleInput battleInput)
        {
            _battleCameraHelper = battleCameraHelper;

            _battleActionController = battleActionController;
            
            _battleInput = battleInput;
        }
        
        private void OnBattleActionChanged(BattleActionContext context)
        {
            _currentCamera.gameObject.SetActive(false);

            if (!_battleCameraHelper.BattleCameras.TryGetValue(context.ActionType, out var newCamera))
            {
                return; 
            }

            _currentCamera = newCamera;
            
            _currentCamera.gameObject.SetActive(true);
        }
        
        public UniTask Load()
        {
            _battleActionController.OnBattleActionChanged += OnBattleActionChanged;

            _currentCamera = _battleCameraHelper.BattleCameras[ActionType.DEFAULT];
            
            return UniTask.CompletedTask;
        }
        

        [CanBeNull]
        public Character GetCharacterWithRaycast()
        {
            var mouseRaycast = _battleCameraHelper.MainCamera.ScreenPointToRay(_battleInput.MousePosition);

            if (!Physics.Raycast(mouseRaycast, out var hit, 1000f, LayerMask.GetMask("Enemy")))
            {
                return null;
            }

            return hit.collider.gameObject.TryGetComponent(out Character selectedCharacter) ? selectedCharacter : null;
        }
    }
}