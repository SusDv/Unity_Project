using System;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerMovingState
{
    public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        stateMachine.ReusableData.MovementSpeedModifier = movementData.WalkData.SpeedModifier;
    }

    public override void OnWalkStarted(InputAction.CallbackContext context)
    {
        base.OnWalkStarted(context);

        stateMachine.ChangeState(stateMachine.RunningState);
    }
}
