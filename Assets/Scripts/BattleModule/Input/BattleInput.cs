using System;
using UnityEngine;

namespace BattleModule.Input
{
    public class BattleInput : MonoBehaviour
    {
        private BattleInputAction BattleInputAction { get; set; }

        private BattleInputAction.ControlsActions BattleControls { get; set; }
        
        public bool MouseLeftButtonPressed { get; private set; }
        
        public int ArrowKeysInput { get; private set; }

        public event Action OnCancelButtonPressed = delegate { };

        public Vector2 MousePosition { get; private set; }
        
        
        private void Awake()
        {
            BattleInputAction = new BattleInputAction();
            
            BattleControls = BattleInputAction.Controls;
        }

        private void Update()
        {
            MouseLeftButtonPressed = GetMouseLeftButtonPressed();
            
            ArrowKeysInput = GetArrowKeysInput();

            CancelButtonPressed();
        }
        
        private bool GetMouseLeftButtonPressed()
        {
            return BattleControls.LeftMouseButton.WasPressedThisFrame();
        }
        
        private int GetArrowKeysInput()
        {
            return (int) (BattleControls.LeftRight.WasPressedThisFrame() ? BattleControls.LeftRight.ReadValue<float>() : 0);
        }

        private void CancelButtonPressed()
        {
            if (!BattleControls.Cancel.WasPressedThisFrame())
            {
                return;
            }
            
            OnCancelButtonPressed?.Invoke();
        }

        private void OnEnable()
        {
            BattleInputAction.Enable();

            BattleControls.MousePosition.performed += MousePosition_Changed;
        }

        private void MousePosition_Changed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            MousePosition = ctx.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            BattleInputAction.Disable();

            BattleControls.MousePosition.performed -= MousePosition_Changed;
        }
    }
}