using System;
using System.Collections.Generic;
using BattleModule.Utility;
using BattleModule.Utility.Enums;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

namespace BattleModule.Controllers 
{
    public class BattleCamera
    {
        private readonly CinemachineVirtualCamera _playersPerspectiveCamera;

        private readonly CinemachineVirtualCamera _playersAllyPerspectiveCamera;

        private readonly Camera _mainCamera;
        
        private readonly LayerMask _characterLayerMask;
        
        private CinemachineVirtualCamera GetPlayerTargetingCamera(TargetType targetType)
        {
            return targetType == TargetType.ALLY ? _playersAllyPerspectiveCamera : _playersPerspectiveCamera;
        }

        public BattleCamera(CinemachineVirtualCamera playersPerspectiveCamera,
            CinemachineVirtualCamera playersAllyPerspectiveCamera,
            Camera mainCamera,
            LayerMask characterLayerMask)
        {
            _playersPerspectiveCamera = playersPerspectiveCamera;

            _playersAllyPerspectiveCamera = playersAllyPerspectiveCamera;
            
            _mainCamera = mainCamera;

            _characterLayerMask = characterLayerMask;
        }

        [CanBeNull]
        public Character GetCharacterWithRaycast()
        {
            var mouseRaycast = _mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (!Physics.Raycast(mouseRaycast, out var hit, 1000f, _characterLayerMask))
            {
                return null;
            }

            return hit.collider.gameObject.TryGetComponent(out Character selectedCharacter) ? selectedCharacter : null;
        }
    }
}