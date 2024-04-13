using System.Linq;
using System.Collections.Generic;
using BattleModule.States.StateMachine;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.States.Base;
using CharacterModule;
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

            BattleStateMachine.BattleController.BattleActionController.OnBattleActionChanged += OnBattleActionChanged;

            StartTurn();

            SetupBattleEvents();

            base.OnEnter();
        }

        public override void OnExit()
        {
            ClearBattleEvents();

            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            SelectCharacterUsingMouse();
            
            SelectCharacterUsingKeys();
        }

        private void SelectCharacterUsingMouse()
        {
            if (!BattleStateMachine.BattleController.BattleInput.MouseLeftButtonPressed)
            {
                return;
            }
            
            BattleStateMachine.BattleController.BattleTargetingController.SetMainTargetWithInput(
                BattleStateMachine.BattleController.BattleCamera.GetCharacterWithRaycast());
        }

        private void SelectCharacterUsingKeys()
        {
            if (BattleStateMachine.BattleController.BattleInput.ArrowKeysInput == 0)
            {
                return;
            }
            
            BattleStateMachine.BattleController.BattleTargetingController.SetMainTargetWithInput(BattleStateMachine.BattleController.BattleInput.ArrowKeysInput);
        }

        private void SetupBattleEvents()
        {
            BattleStateMachine.BattleController.BattleEventManager.OnActionButtonPressed += BattleActionHandler;
        }

        private void ClearBattleEvents() 
        {
            BattleStateMachine.BattleController.BattleEventManager.OnActionButtonPressed -= BattleActionHandler;
        }
        
        private void StartTurn()
        {
            BattleStateMachine.BattleController.BattleTurnController.StartTurn();  
        }

        private void OnBattleActionChanged(BattleActionContext context)
        {
            BattleStateMachine.BattleController.BattleTargetingController.SetTargetingData(context);
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
