using BattleModule.States.StateMachine;
using CharacterModule.StateMachine;

namespace BattleModule.States.Base
{
    public class BattleState : IState
    {
        protected readonly BattleStateMachine BattleStateMachine;

        protected int ArrowKeysInput;

        protected bool CancelKeyPressed;

        protected bool MousePressed;

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
            ArrowKeysInput = GetArrowKeyInput();
            
            CancelKeyPressed = GetCancelKeyInput();

            MousePressed = GetMouseInput();
        }

        private bool GetMouseInput()
        {
            return BattleStateMachine.BattleController.BattleInput.BattleInputAction.BattleInput.LeftMouseButton
                .WasPressedThisFrame();
        }

        private int GetArrowKeyInput()
        {
            return BattleStateMachine.BattleController.BattleInput.BattleInputAction.BattleInput.RightArrow.WasPressedThisFrame() ? 1 :
               BattleStateMachine.BattleController.BattleInput.BattleInputAction.BattleInput.LeftArrow.WasPressedThisFrame() ? -1 : 0;
        }

        private bool GetCancelKeyInput() 
        {
            return BattleStateMachine.BattleController.BattleInput.BattleInputAction.BattleInput.Cancel.WasPressedThisFrame() ? true : false;
        }
    }
}