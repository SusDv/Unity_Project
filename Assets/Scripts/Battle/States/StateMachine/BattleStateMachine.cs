using BattleModule.StateMachineBase.States;
using BattleModule.Controllers.Core;

namespace BattleModule.StateMachineBase 
{
    public class BattleStateMachine : StateMachine
    {
        public IState CurrentState;
        public BattleController BattleController { get; }

        public BattleTargetingState BattleTargetingState { get; }

        public BattleEnemyAttackState BattleEnemyAttackState { get; }

        public BattleStateMachine(BattleController battleController)
        {
            BattleController = battleController;

            BattleTargetingState = new BattleTargetingState(this);

            BattleEnemyAttackState = new BattleEnemyAttackState(this);
        }
    }
}