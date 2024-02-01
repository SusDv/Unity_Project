using BattleModule.Controllers.Base;
using CharacterModule.StateMachine;

namespace BattleModule.States.StateMachine 
{
    public class BattleStateMachine : StateMachineBase
    {
        public IState CurrentState;

        public BattleFightController BattleController { get; }

        public BattlePlayerActionState BattlePlayerActionState { get; }

        public BattleEnemyAttackState BattleEnemyAttackState { get; }

        public BattleStateMachine(BattleFightController battleController)
        {
            BattleController = battleController;

            BattlePlayerActionState = new BattlePlayerActionState(this);

            BattleEnemyAttackState = new BattleEnemyAttackState(this);
        }
    }
}