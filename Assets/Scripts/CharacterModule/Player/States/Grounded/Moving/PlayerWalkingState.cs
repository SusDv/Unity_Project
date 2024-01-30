using System;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerMovingState
{
    public PlayerWalkingState(PlayerMovementStateMachineBase playerMovementStateMachineBase) : base(playerMovementStateMachineBase)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        StateMachineBase.ReusableData.MovementSpeedModifier = movementData.WalkData.SpeedModifier;
    }

    public override void OnWalkStarted(InputAction.CallbackContext context)
    {
        base.OnWalkStarted(context);

        StateMachineBase.ChangeState(StateMachineBase.RunningState);
    }
}
