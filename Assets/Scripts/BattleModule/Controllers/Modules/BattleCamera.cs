using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BattleModule.Actions.Context;
using BattleModule.Input;
using BattleModule.Utility;
using CharacterModule.Types;
using CharacterModule.Types.Base;
using Cinemachine;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.Controllers.Modules 
{
    public class BattleCamera : ILoadingUnit<List<Character>>
    {
        private readonly BattleCameraHelper _battleCameraHelper;

        private readonly BattleActionController _battleActionController;

        private readonly BattleTargetingController _battleTargetingController;

        private readonly BattleInput _battleInput;

        private CancellationTokenSource _cancellationTokenSource;

        private Vector3 _initialCameraPosition;
        
        private float _initialCameraOrbitRotation;

        private List<Character> _charactersOnScene;

        private Vector3 _centerPosition;

        private const float ROTATION_OFFSET = -2.671f;

        [Inject]
        public BattleCamera(BattleCameraHelper battleCameraHelper,
            BattleActionController battleActionController,
            BattleTargetingController battleTargetingController,
            BattleInput battleInput)
        {
            _battleCameraHelper = battleCameraHelper;

            _battleActionController = battleActionController;

            _battleTargetingController = battleTargetingController;
            
            _battleInput = battleInput;
        }

        private void OnBattleActionChanged(BattleActionContext battleActionContext)
        {
            _initialCameraPosition = _battleCameraHelper.Cameras[BattleCameraHelper.CameraType.PLAYER_DEFAULT_ACTION]
                .transform.position;
        }

        private void OnTargetsChanged(List<Character> targets)
        {
            var cameraTransform = _battleCameraHelper.Cameras[BattleCameraHelper.CameraType.PLAYER_DEFAULT_ACTION].transform;
            
            var targetTransform = targets[targets.Count / 2].transform;

            SmoothRotateTowardsAsync(cameraTransform, targetTransform);
        }
        
        private void SmoothRotateTowardsAsync(Transform cameraTransform, Transform targetTransform)
        {
            var targetRotation = CalculateTargetRotation(cameraTransform);

            targetRotation.eulerAngles = new Vector3(targetRotation.eulerAngles.x,
                targetRotation.eulerAngles.y + ROTATION_OFFSET, targetRotation.eulerAngles.z);
                
            cameraTransform.rotation = targetRotation;
        }

        private Quaternion CalculateTargetRotation(Component cameraTransform)
        {
            var difference =  -_initialCameraPosition + _centerPosition;
            
            var targetRotation = Quaternion.LookRotation(difference);
            
            var eulerAngles = cameraTransform.transform.eulerAngles;
            
            targetRotation = Quaternion.Euler(eulerAngles.x, targetRotation.eulerAngles.y, eulerAngles.z);
            
            return targetRotation;
        }

        public UniTask Load(List<Character> characters)
        {
            _battleActionController.OnBattleActionChanged += OnBattleActionChanged;

            _battleTargetingController.OnTargetsChanged += OnTargetsChanged;

            var enemyCharacters = characters.Where(c => c is Enemy).ToList();
            
            _centerPosition = (enemyCharacters[0].transform.position + enemyCharacters[^1].transform.position) * 0.5f;
            
            return UniTask.CompletedTask;
        }
        
        [CanBeNull]
        public Character GetCharacterWithRaycast()
        {
            var mouseRaycast = _battleCameraHelper.MainCamera.ScreenPointToRay(_battleInput.MousePosition);

            if (!Physics.Raycast(mouseRaycast, out var hit, 1000f, LayerMask.GetMask("Character")))
            {
                return null;
            }

            return hit.collider.gameObject.TryGetComponent(out Character selectedCharacter) ? selectedCharacter : null;
        }
    }
}