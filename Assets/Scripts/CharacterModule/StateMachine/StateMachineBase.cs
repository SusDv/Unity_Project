namespace CharacterModule.StateMachine
{
    public abstract class StateMachineBase
    {
        protected IState currentState;

        public void ChangeState(IState state) 
        {
            currentState?.OnExit();

            currentState = state;
            
            currentState.OnEnter();
        }
    }
}
