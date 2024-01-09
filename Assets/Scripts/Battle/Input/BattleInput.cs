using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInput : MonoBehaviour
{
    public BattleInputAction BattleInputAction { get; private set; }

    public BattleInputAction.BattleInputActions BattleActions { get; private set; }

    public Vector2 MousePosition { get; private set; }


    private void Awake()
    {
        BattleInputAction = new BattleInputAction();

        BattleActions = BattleInputAction.BattleInput;
    }

    private void OnEnable()
    {
        BattleActions.Enable();

        BattleActions.MousePosition.performed += MousePosition_Changed;
    }

    private void MousePosition_Changed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        MousePosition = ctx.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        BattleActions.Disable();

        BattleActions.MousePosition.performed -= MousePosition_Changed;
    }
}
