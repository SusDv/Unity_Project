using BattleModule.States.Base;
using BattleModule.States.StateMachine;

namespace BattleModule.States
{
    public class BattlePrepareState : BattleState
    {
        public BattlePrepareState(BattleStateMachine battleStateMachine) 
            : base(battleStateMachine)
        { }

        private void PrepareBattle()
        {
            BattleStateMachine.BattleController.BattleTurnEvents.BattleInit();
        }
        
        public override void OnEnter()
        {
            PrepareBattle();

            BattleStateMachine.ChangeState(BattleStateMachine.BattleDeciderState);
        }
    }
}