using BattleModule.States.StateMachine;
using CharacterModule.StateMachine;

namespace BattleModule.States.Base
{
    public class BattleState : IState
    {
        protected readonly BattleStateMachine BattleStateMachine;

        protected BattleState(BattleStateMachine battleStateMachine)
        {
            BattleStateMachine = battleStateMachine;
        }

        public virtual void OnEnter()
        {
            BattleStateMachine.CurrentState = this;
        }

        public virtual void OnExit() { }

        public virtual void OnFixedUpdate() { }

        public virtual void OnUpdate()
        {
           
        }
    }
}