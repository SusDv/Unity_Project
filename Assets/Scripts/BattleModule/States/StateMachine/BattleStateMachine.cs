using BattleModule.Controllers;
using CharacterModule.StateMachine;

namespace BattleModule.States.StateMachine 
{
    public class BattleStateMachine : StateMachineBase
    {
        public BattleController BattleController { get; }
        
        public BattlePrepareState BattlePrepareState { get; }
        public BattleDeciderState BattleDeciderState { get; }
        
        public BattlePlayerActionState BattlePlayerActionState { get; }

        public BattleEnemyAttackState BattleEnemyAttackState { get; }

        public BattleStateMachine(BattleController battleController)
        {
            BattleController = battleController;

            BattlePrepareState = new BattlePrepareState(this);

            BattlePlayerActionState = new BattlePlayerActionState(this);

            BattleEnemyAttackState = new BattleEnemyAttackState(this);
            
            BattleDeciderState = new BattleDeciderState(this);
        }
    }
}