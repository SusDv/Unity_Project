using BattleModule.Actions.Context;
using BattleModule.Input;
using BattleModule.Utility;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.Controllers.Modules 
{
    public class BattleCamera : ILoadingUnit
    {
        private readonly BattleUIHelper _battleUIHelper;

        private readonly BattleActionController _battleActionController;

        private readonly BattleInput _battleInput;

        [Inject]
        public BattleCamera(BattleUIHelper battleUIHelper,
            BattleActionController battleActionController,
            BattleInput battleInput)
        {
            _battleUIHelper = battleUIHelper;

            _battleActionController = battleActionController;
            
            _battleInput = battleInput;
        }

        public UniTask Load()
        {
            _battleActionController.OnBattleActionChanged += OnBattleActionChanged;
            
            return UniTask.CompletedTask;
        }

        private void OnBattleActionChanged(BattleActionContext context)
        {
            
        }

        [CanBeNull]
        public Character GetCharacterWithRaycast()
        {
            var mouseRaycast = _battleUIHelper.MainCamera.ScreenPointToRay(_battleInput.MousePosition);

            if (!Physics.Raycast(mouseRaycast, out var hit, 1000f, LayerMask.GetMask("Character")))
            {
                return null;
            }

            return hit.collider.gameObject.TryGetComponent(out Character selectedCharacter) ? selectedCharacter : null;
        }
    }
}