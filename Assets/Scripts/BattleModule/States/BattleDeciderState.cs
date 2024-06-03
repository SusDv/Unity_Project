using BattleModule.States.Base;
using BattleModule.States.StateMachine;

namespace BattleModule.States
{
    public class BattleDeciderState : BattleState
    {
        public BattleDeciderState(BattleStateMachine battleStateMachine) 
            : base(battleStateMachine)
        { }

        private void PrepareNextTurn()
        {
            BattleStateMachine.BattleController.BattleTargetingController.ResetTargetIndex();
            
            BattleStateMachine.BattleController.BattleTurnController.BeginTurn();
            
            BattleStateMachine.BattleController.BattleActionController.SetDefaultBattleAction();
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            PrepareNextTurn();
            
            if (BattleStateMachine.BattleController.BattleTurnController.IsNextEnemy)
            {
                BattleStateMachine.ChangeState(BattleStateMachine.BattleEnemyAttackState);
                
                return;
            }
            
            BattleStateMachine.ChangeState(BattleStateMachine.BattlePlayerActionState);
        }
    }
}