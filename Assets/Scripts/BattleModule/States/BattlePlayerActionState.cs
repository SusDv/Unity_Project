using System.Linq;
using System.Collections.Generic;
using BattleModule.States.StateMachine;
using BattleModule.States.Base;
using CharacterModule.CharacterType.Base;

namespace BattleModule.States
{
    public class BattlePlayerActionState : BattleState
    {
        private List<Character> _currentTargets;
        
        public BattlePlayerActionState(BattleStateMachine battleStateMachine) 
            : base(battleStateMachine)
        {}

        public override void OnEnter()
        {
            _currentTargets = new List<Character>();
            
            StartTurn();

            SetupBattleEvents();

            base.OnEnter();
        }

        public override void OnExit()
        {
            ClearBattleEvents();

            base.OnExit();
        }

        private void SelectCharacterUsingMouse()
        {
            BattleStateMachine.BattleController.BattleTargetingController.SetMainTargetWithInput(
                BattleStateMachine.BattleController.BattleCamera.GetCharacterWithRaycast());
        }

        private void SelectCharacterUsingKeys(int direction)
        {
            BattleStateMachine.BattleController.BattleTargetingController.SetMainTargetWithInput(direction);
        }

        private void SetupBattleEvents()
        {
            BattleStateMachine.BattleController.BattleEventManager.OnActionButtonPressed += BattleActionHandler;

            BattleStateMachine.BattleController.BattleInput.OnMouseButtonPressed += SelectCharacterUsingMouse;
            
            BattleStateMachine.BattleController.BattleInput.OnArrowsKeyPressed += SelectCharacterUsingKeys;
        }

        private void ClearBattleEvents() 
        {
            BattleStateMachine.BattleController.BattleEventManager.OnActionButtonPressed -= BattleActionHandler;
            
            BattleStateMachine.BattleController.BattleInput.OnMouseButtonPressed += SelectCharacterUsingMouse;
            
            BattleStateMachine.BattleController.BattleInput.OnArrowsKeyPressed += SelectCharacterUsingKeys;
        }
        
        private void StartTurn()
        {
            BattleStateMachine.BattleController.BattleTurnController.StartTurn();  
            
            BattleStateMachine.BattleController.BattleActionController.SetDefaultBattleAction();
        }

        private void BattleActionHandler()
        {
            _currentTargets = BattleStateMachine.BattleController.BattleTargetingController.GetSelectedTargets();
            
            if (!BattleStateMachine.BattleController.BattleTargetingController.IsReadyForAction())
            {
                return;
            }
            
            BattleStateMachine.BattleController.BattleActionController.ExecuteBattleAction(_currentTargets.ToList());

            BattleStateMachine.ChangeState(BattleStateMachine.BattlePlayerActionState);
        }
    }
}
