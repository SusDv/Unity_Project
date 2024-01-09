using UnityEngine.InputSystem;

public class PlayerSprintingState : PlayerMovingState
{
    public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        stateMachine.ReusableData.MovementSpeedModifier = movementData.SprintData.SpeedModifier;
    }

    public override void OnWalkStarted(InputAction.CallbackContext context)
    {
        base.OnWalkStarted(context);

        stateMachine.ChangeState(stateMachine.WalkingState);
    }
}
