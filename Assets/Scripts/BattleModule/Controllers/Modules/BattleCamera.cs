using BattleModule.Input;
using CharacterModule.CharacterType.Base;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace BattleModule.Controllers.Modules 
{
    public class BattleCamera
    {
        private readonly Camera _mainCamera;

        private readonly BattleInput _battleInput;

        [Inject]
        public BattleCamera(Camera mainCamera, BattleInput battleInput)
        {
            _mainCamera = mainCamera;

            _battleInput = battleInput;
        }

        [CanBeNull]
        public Character GetCharacterWithRaycast()
        {
            var mouseRaycast = _mainCamera.ScreenPointToRay(_battleInput.MousePosition);

            if (!Physics.Raycast(mouseRaycast, out var hit, 1000f, LayerMask.GetMask("Character")))
            {
                return null;
            }

            return hit.collider.gameObject.TryGetComponent(out Character selectedCharacter) ? selectedCharacter : null;
        }
    }
}