using BattleModule.Controllers;
using CharacterModule.StateMachine;

namespace BattleModule.States.StateMachine 
{
    public class BattleStateMachine : StateMachineBase
    {
        public IState CurrentState;

        public BattleController BattleController { get; }

        public BattlePlayerActionState BattlePlayerActionState { get; }

        public BattleEnemyAttackState BattleEnemyAttackState { get; }

        public BattleStateMachine(BattleController battleController)
        {
            BattleController = battleController;

            BattlePlayerActionState = new BattlePlayerActionState(this);

            BattleEnemyAttackState = new BattleEnemyAttackState(this);
        }
    }
}