using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerMovingState
{
    public PlayerRunningState(PlayerMovementStateMachineBase playerMovementStateMachineBase) : base(playerMovementStateMachineBase)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        StateMachineBase.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;
    }
    public override void OnWalkStarted(InputAction.CallbackContext context)
    {
        base.OnWalkStarted(context);

        StateMachineBase.ChangeState(StateMachineBase.WalkingState);
    }
}
