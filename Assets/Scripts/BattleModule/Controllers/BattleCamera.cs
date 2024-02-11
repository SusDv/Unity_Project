using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace BattleModule.Controllers 
{
    public class BattleCamera
    {
        private readonly Camera _mainCamera;

        [Inject]
        public BattleCamera(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        [CanBeNull]
        public Character GetCharacterWithRaycast()
        {
            var mouseRaycast = _mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (!Physics.Raycast(mouseRaycast, out var hit, 1000f, LayerMask.GetMask("Character")))
            {
                return null;
            }

            return hit.collider.gameObject.TryGetComponent(out Character selectedCharacter) ? selectedCharacter : null;
        }
    }
}