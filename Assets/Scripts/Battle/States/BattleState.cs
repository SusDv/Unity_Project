public class BattleState : IState
{
    protected BattleStateMachine battleStateMachine;

    protected int ArrowKeysInput;

    public BattleState(BattleStateMachine battleStateMachine)
    {
        this.battleStateMachine = battleStateMachine;
    }

    public virtual void OnEnter()
    {
        battleStateMachine.CurrentState = this;
    }

    public virtual void OnExit() { }

    public virtual void OnFixedUpdate() { }

    public virtual void OnUpdate()
    {
        ArrowKeysInput = GetArrowKeyInput();
    }

    private int GetArrowKeyInput()
    {
        return battleStateMachine.BattleController.BattleInput.BattleInputAction.BattleInput.RightArrow.WasPressedThisFrame() ? 1 :
           battleStateMachine.BattleController.BattleInput.BattleInputAction.BattleInput.LeftArrow.WasPressedThisFrame() ? -1 : 0;
    }
}
