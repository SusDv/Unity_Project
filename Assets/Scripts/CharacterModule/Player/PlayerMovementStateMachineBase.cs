using CharacterModule.StateMachine;

public class PlayerMovementStateMachineBase : StateMachineBase
{
    public PlayerMovementController PlayerMovementController { get; }

    public PlayerStateReusableData ReusableData { get; }

    public PlayerIdleState IdleState { get; }
    public PlayerWalkingState WalkingState { get; }
    public PlayerRunningState RunningState { get; }
    public PlayerSprintingState SprintingState { get; }
    public PlayerJumpingState JumpingState { get; }

    public PlayerMovementStateMachineBase(PlayerMovementController playerMovementController) 
    {
        PlayerMovementController = playerMovementController;

        ReusableData = new PlayerStateReusableData();

        IdleState = new PlayerIdleState(this);
        WalkingState = new PlayerWalkingState(this);
        RunningState = new PlayerRunningState(this);
        SprintingState = new PlayerSprintingState(this);

        JumpingState = new PlayerJumpingState(this);
    }
}
