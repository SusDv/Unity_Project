using UnityEngine;
public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        stateMachine.ReusableData.MovementSpeedModifier = 0f;

        ResetVelocity();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (stateMachine.ReusableData.MovementInput == Vector2.zero) 
        {
            return;
        }

        OnMove();
    }
}
