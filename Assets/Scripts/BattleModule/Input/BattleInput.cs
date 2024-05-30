using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;

namespace BattleModule.Input
{
    public class BattleInput : MonoBehaviour, ILoadingUnit
    {
        private BattleInputAction BattleInputAction { get; set; }
        
        private BattleInputAction.ControlsActions BattleControls { get; set; }
        
        public event Action OnMouseButtonPressed  = delegate { };

        public event Action<int> OnArrowsKeyPressed = delegate { };

        public event Action OnCancelButtonPressed = delegate { };

        public Vector2 MousePosition { get; private set; }
        

        private bool _isLoaded;

        public UniTask Load()
        {
            BattleInputAction = new BattleInputAction();
            
            BattleControls = BattleInputAction.Controls;
            
            BattleInputAction.Enable();

            BattleControls.MousePosition.performed += MousePosition_Changed;
            
            _isLoaded = true;
            
            return UniTask.CompletedTask;
        }

        private void Update()
        {
            if (!_isLoaded)
            {
                return;
            }

            GetMouseLeftButtonPressed();
            
            GetArrowKeysInput();

            CancelButtonPressed();
        }
        
        private void GetMouseLeftButtonPressed()
        {
            if (BattleControls.LeftMouseButton.WasPressedThisFrame())
            {
                OnMouseButtonPressed?.Invoke();
            }
        }
        
        private void GetArrowKeysInput()
        {
            if (BattleControls.LeftRight.WasPressedThisFrame())
            {
                OnArrowsKeyPressed?.Invoke((int) BattleControls.LeftRight.ReadValue<float>());
            }
        }

        private void CancelButtonPressed()
        {
            if (BattleControls.Cancel.WasPressedThisFrame())
            {
                OnCancelButtonPressed?.Invoke();
            }
        }

        private void MousePosition_Changed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            MousePosition = ctx.ReadValue<Vector2>();
        }
    }
}