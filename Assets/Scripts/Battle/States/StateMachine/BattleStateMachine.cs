public class BattleStateMachine : StateMachine
{
    public IState CurrentState;
    public BattleController BattleController { get; }

    public BattleIdleState BattleIdleState { get; }

    public BattleTargetingState BattleTargetingState { get; }

    public BattleEnemyAttackState BattleEnemyAttackState { get; }


    public BattleStateMachine(BattleController battleController) 
    {
        BattleController = battleController;

        BattleIdleState = new BattleIdleState(this);

        BattleTargetingState = new BattleTargetingState(this);

        BattleEnemyAttackState = new BattleEnemyAttackState(this);
    }
}
