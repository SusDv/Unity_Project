using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Controllers.Targeting.Processor;
using BattleModule.States.Base;
using BattleModule.States.StateMachine;

namespace BattleModule.States
{
    public class BattlePlayerActionState : BattleState
    {
        private Stack<Character> _currentTargets;
        

        public BattlePlayerActionState(BattleStateMachine battleStateMachine) 
            : base(battleStateMachine)
        {}

        public override void OnEnter()
        {
            _currentTargets = new Stack<Character>();

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

            SelectCharacterUsingKeys();

            CheckCancelKeyPressed();
        }

        private void SelectCharacterUsingKeys()
        {
            if (ArrowKeysInput == 0)
            {
                return;
            }
            
            BattleStateMachine.BattleController.BattleTargetingController.SetNeighbourAsMainTarget(ArrowKeysInput);
        }

        private void SetupBattleEvents()
        {
            BattleEventManager.Instance.OnActionButtonPressed += BattleActionHandler;
        }

        private void ClearBattleEvents() 
        {
            BattleEventManager.Instance.OnActionButtonPressed -= BattleActionHandler;
        }
        
        private void StartTurn()
        {
            BattleStateMachine.BattleController.BattleTurnController.TurnEffects();  
        }

        private void OnBattleActionChanged(BattleActionContext context)
        {
            BattleTargetingProcessor.SetCurrentSearchType(context.TargetSearchType);
            
            BattleStateMachine.BattleController.BattleTargetingController.SetTargetingData(context.TargetType, context.MaxTargetCount);
        }

        private void BattleActionHandler()
        {
            if (!BattleStateMachine.BattleController.BattleTargetingController.IsReadyForAction(ref _currentTargets))
            {
                return;
            }
            
            BattleStateMachine.BattleController.BattleActionController.ExecuteBattleAction(_currentTargets.ToList());

            BattleStateMachine.ChangeState(BattleStateMachine.BattlePlayerActionState);
        }

        private void CheckCancelKeyPressed()
        {
            if (CancelKeyPressed)
            {

            }
        }
    }
}
