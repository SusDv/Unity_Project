using BattleModule.ActionCore.Events;
using BattleModule.StateMachineBase.States.Core;

namespace BattleModule.StateMachineBase.States
{
    public class BattleIdleState : BattleState
    {
        public BattleIdleState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
        {

        }

        public override void OnEnter()
        {
            _battleStateMachine.BattleController.OnCharacterTargetChanged?.Invoke(UnityEngine.Vector3.zero);
            
            _battleStateMachine.BattleController.BattleCharactersInTurn.ResetCharacterInTurnBattlePoints();

            _battleStateMachine.BattleController.BattleCharactersInTurn.TriggerCharacterInTurnTemporaryModifiers();

            
            BattleGlobalActionEventProcessor.OnBattleAction += BattleActionHandler;
            base.OnEnter();
        }
        public override void OnExit()
        {
            BattleGlobalActionEventProcessor.OnBattleAction -= BattleActionHandler;
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        private void BattleActionHandler()
        {
            _battleStateMachine.ChangeState(_battleStateMachine.BattleTargetingState);
        }
    }
}