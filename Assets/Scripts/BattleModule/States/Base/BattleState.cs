using BattleModule.Controllers.Modules;
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
        
        private void OnBattleActionFinished(BattleActionController.ActionResult actionResult)
        {
            BattleStateMachine.BattleController.BattleTurnEvents.AdvanceTurn();
            
            BattleStateMachine.BattleController.BattleActionController.OnBattleActionFinished -= OnBattleActionFinished;
            
            BattleStateMachine.ChangeState(BattleStateMachine.BattleDeciderState);
        }

        public virtual void OnEnter()
        {
            BattleStateMachine.BattleController.BattleActionController.OnBattleActionFinished += OnBattleActionFinished;
        }

        public virtual void OnExit()
        {
            
        }
    }
}