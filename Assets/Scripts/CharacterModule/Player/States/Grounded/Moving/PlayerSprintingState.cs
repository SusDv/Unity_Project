using UnityEngine.InputSystem;

public class PlayerSprintingState : PlayerMovingState
{
    public PlayerSprintingState(PlayerMovementStateMachineBase playerMovementStateMachineBase) : base(playerMovementStateMachineBase)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        StateMachineBase.ReusableData.MovementSpeedModifier = movementData.SprintData.SpeedModifier;
    }

    public override void OnWalkStarted(InputAction.CallbackContext context)
    {
        base.OnWalkStarted(context);

        StateMachineBase.ChangeState(StateMachineBase.WalkingState);
    }
}
