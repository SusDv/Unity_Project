using System.Collections.Generic;
using BattleModule.States.Base;
using BattleModule.States.StateMachine;
using CharacterModule.Types.Base;

namespace BattleModule.States
{
    public class BattleEnemyAttackState : BattleState
    {
        private List<Character> _currentTargets;
        
        public BattleEnemyAttackState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
        { }

        private async void AttackRandomTarget()
        { 
            await BattleStateMachine.BattleController.BattleActionController.ExecuteBattleAction(_currentTargets);
        }

        private void PrepareRandomTargets()
        {
            BattleStateMachine.BattleController.BattleTargetingController.SetMainTargetWithInput();

            _currentTargets = BattleStateMachine.BattleController.BattleTargetingController.GetSelectedTargets();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            PrepareRandomTargets();
            
            AttackRandomTarget();
        }
    }
}