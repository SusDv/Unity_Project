using System.Collections.Generic;
using System.Linq;
using BattleModule.Utility.Interfaces;
using UnityEngine;

namespace BattleModule.Input
{
    public class BattleInput : MonoBehaviour
    {
        private BattleInputAction BattleInputAction { get; set; }

        private BattleInputAction.ControlsActions BattleControls { get; set; }
        
        public bool MouseLeftButtonPressed { get; private set; }
        
        public int ArrowKeysInput { get; private set; }
        
        public Vector2 MousePosition { get; private set; }

        private List<IBattleCancelable> _cancelableList = new();
        
        public void AddCancelable(IBattleCancelable battleCancelable)
        {
            _cancelableList.Add(battleCancelable);
        }

        public void PrependCancelable(IBattleCancelable battleCancelable)
        {
            _cancelableList = _cancelableList.Prepend(battleCancelable).ToList();
        }

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
            
            HandleCancelAction();
        }

        private void HandleCancelAction()
        {
            for (int i = _cancelableList.Count - 1; i >= 0; i--)
            {
                if (!_cancelableList[i].Cancel())
                {
                    break;
                }
            }
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