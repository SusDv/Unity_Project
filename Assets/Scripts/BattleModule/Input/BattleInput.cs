using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleModule.Utility;
using BattleModule.Utility.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BattleModule.Input
{
    public class BattleInput : MonoBehaviour, ILoadingUnit
    {
        private BattleInputAction BattleInputAction { get; set; }
        
        private BattleInputAction.ControlsActions BattleControls { get; set; }
        
        public Action OnMouseButtonPressed  = delegate { };

        public Action<int> OnArrowsKeyPressed = delegate { };
        
        public Vector2 MousePosition { get; private set; }
        

        private List<IBattleCancelable> _cancelableList = new();

        private bool isLoaded;
        
        public void AppendCancelable(IBattleCancelable battleCancelable)
        {
            _cancelableList.Add(battleCancelable);
        }

        public void PrependCancelable(IBattleCancelable battleCancelable)
        {
            _cancelableList = _cancelableList.Prepend(battleCancelable).ToList();
        }

        public UniTask Load()
        {
            BattleInputAction = new BattleInputAction();
            
            BattleControls = BattleInputAction.Controls;
            
            BattleInputAction.Enable();

            BattleControls.MousePosition.performed += MousePosition_Changed;
            
            isLoaded = true;
            
            return UniTask.CompletedTask;
        }

        private void Update()
        {
            if (!isLoaded)
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

        private void MousePosition_Changed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            MousePosition = ctx.ReadValue<Vector2>();
        }
    }
}