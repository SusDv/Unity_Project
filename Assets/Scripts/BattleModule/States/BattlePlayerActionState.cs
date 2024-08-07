using System.Collections.Generic;
using BattleModule.States.StateMachine;
using BattleModule.States.Base;
using CharacterModule.Types.Base;

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
            StartTurn();

            SetupBattleEvents();
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
            BattleStateMachine.BattleController.BattleTurnEvents.OnActionInvoked += ActionHandler;

            BattleStateMachine.BattleController.BattleInput.OnMouseButtonPressed += SelectCharacterUsingMouse;
            
            BattleStateMachine.BattleController.BattleInput.OnArrowsKeyPressed += SelectCharacterUsingKeys;
        }

        private void ClearBattleEvents() 
        {
            BattleStateMachine.BattleController.BattleTurnEvents.OnActionInvoked -= ActionHandler;
            
            BattleStateMachine.BattleController.BattleInput.OnMouseButtonPressed -= SelectCharacterUsingMouse;
            
            BattleStateMachine.BattleController.BattleInput.OnArrowsKeyPressed -= SelectCharacterUsingKeys;
        }
        
        private void StartTurn()
        {
            _currentTargets = new List<Character>();
        }

        private async void ActionHandler()
        {
            _currentTargets = BattleStateMachine.BattleController.BattleTargetingController.GetSelectedTargets();
            
            if (!BattleStateMachine.BattleController.BattleTargetingController.IsReadyForAction())
            {
                return;
            }
            
            ClearBattleEvents();
            
            await BattleStateMachine.BattleController.BattleActionController.ExecuteBattleAction(_currentTargets);
        }
    }
}
