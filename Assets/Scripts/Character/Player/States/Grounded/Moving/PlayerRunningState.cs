using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerMovingState
{
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;
    }
    public override void OnWalkStarted(InputAction.CallbackContext context)
    {
        base.OnWalkStarted(context);

        stateMachine.ChangeState(stateMachine.WalkingState);
    }
}
