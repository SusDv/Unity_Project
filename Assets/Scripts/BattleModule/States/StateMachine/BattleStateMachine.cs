using BattleModule.Controllers.Base;
using CharacterModule.StateMachine;

namespace BattleModule.States.StateMachine 
{
    public class BattleStateMachine : StateMachineBase
    {
        public IState CurrentState;

        public BattleFightController BattleController { get; }

        public BattleTargetingState BattleTargetingState { get; }

        public BattleEnemyAttackState BattleEnemyAttackState { get; }

        public BattleStateMachine(BattleFightController battleController)
        {
            BattleController = battleController;

            BattleTargetingState = new BattleTargetingState(this);

            BattleEnemyAttackState = new BattleEnemyAttackState(this);
        }
    }
}