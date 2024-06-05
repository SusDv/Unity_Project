using System.Collections.Generic;
using BattleModule.Actions.Outcome;
using BattleModule.States.StateMachine;
using CharacterModule.StateMachine;
using CharacterModule.Types.Base;

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
            BattleStateMachine.BattleController.BattleActionController.OnBattleActionFinished += OnBattleActionFinished;
        }

        public virtual void OnExit()
        {
            BattleStateMachine.BattleController.BattleActionController.OnBattleActionFinished -= OnBattleActionFinished;
        }

        public virtual void OnFixedUpdate() { }

        public virtual void OnUpdate() { }

        private void OnBattleActionFinished(List<Character> targets, IReadOnlyList<BattleActionOutcome> outcomes)
        {
            BattleStateMachine.BattleController.BattleTurnEvents.AdvanceTurn();
            
            BattleStateMachine.ChangeState(BattleStateMachine.BattleDeciderState);
        }
    }
}