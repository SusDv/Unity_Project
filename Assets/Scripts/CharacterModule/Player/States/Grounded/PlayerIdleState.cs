using UnityEngine;
public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerMovementStateMachineBase playerMovementStateMachineBase) : base(playerMovementStateMachineBase)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        StateMachineBase.ReusableData.MovementSpeedModifier = 0f;

        ResetVelocity();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (StateMachineBase.ReusableData.MovementInput == Vector2.zero) 
        {
            return;
        }

        OnMove();
    }
}
