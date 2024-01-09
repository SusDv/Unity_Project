using BattleModule.ActionCore.Events;

public class BattleIdleState : BattleState
{
    public BattleIdleState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }

    public override void OnEnter()
    {
        battleStateMachine.BattleController.OnCharacterTargetChanged?.Invoke(UnityEngine.Vector3.zero);

        BattleGlobalActionEvent.OnBattleAction += BattleActionHandler;
        base.OnEnter();       
    }
    public override void OnExit()
    {
        BattleGlobalActionEvent.OnBattleAction -= BattleActionHandler;
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    private void BattleActionHandler() 
    {
        battleStateMachine.ChangeState(battleStateMachine.BattleTargetingState);
    }
}
