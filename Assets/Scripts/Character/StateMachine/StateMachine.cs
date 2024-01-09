public abstract class StateMachine
{
    protected IState currentState;

    public void ChangeState(IState state) 
    {
        currentState?.OnExit();

        currentState = state;
        currentState.OnEnter();
    }

    public void OnUpdate() 
    {
        currentState?.OnUpdate();
    }
    public void OnFixedUpdate() 
    {
        currentState?.OnFixedUpdate();
    }
}
