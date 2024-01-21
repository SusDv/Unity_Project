namespace BattleModule.StateMachineBase.States.Core
{
    public class BattleState : IState
    {
        protected BattleStateMachine _battleStateMachine;

        protected int _arrowKeysInput;

        protected bool _cancelKeyPressed;

        public BattleState(BattleStateMachine battleStateMachine)
        {
            _battleStateMachine = battleStateMachine;
        }

        public virtual void OnEnter()
        {
            _battleStateMachine.CurrentState = this;
        }

        public virtual void OnExit() { }

        public virtual void OnFixedUpdate() { }

        public virtual void OnUpdate()
        {
            _arrowKeysInput = GetArrowKeyInput();
            _cancelKeyPressed = GetCancelKeyInput();
        }

        private int GetArrowKeyInput()
        {
            return _battleStateMachine.BattleController.BattleInput.BattleInputAction.BattleInput.RightArrow.WasPressedThisFrame() ? 1 :
               _battleStateMachine.BattleController.BattleInput.BattleInputAction.BattleInput.LeftArrow.WasPressedThisFrame() ? -1 : 0;
        }

        private bool GetCancelKeyInput() 
        {
            return _battleStateMachine.BattleController.BattleInput.BattleInputAction.BattleInput.Cancel.WasPressedThisFrame() ? true : false;
        }
    }
}