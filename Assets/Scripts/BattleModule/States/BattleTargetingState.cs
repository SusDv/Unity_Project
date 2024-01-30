using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Controllers.Targeting.Processor;
using BattleModule.States.Base;
using BattleModule.States.StateMachine;
using BattleModule.Utility.Enums;

namespace BattleModule.States
{
    public class BattleTargetingState : BattleState
    {
        private Character _mainTarget;

        private Stack<Character> _currentTargets;

        private int _currentTargetIndex;

        private BattleActionContext _battleActionContext;

        public BattleTargetingState(BattleStateMachine battleStateMachine) 
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

            _currentTargetIndex = BattleStateMachine.BattleController.BattleCharactersOnScene.GetNearbyCharacterOnSceneIndex(_mainTarget, ArrowKeysInput);

            SetTargets(_battleActionContext.TargetType, _battleActionContext.MaxTargetCount);
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
            _currentTargetIndex = -1;

            BattleStateMachine.BattleController.BattleTurnController.TurnEffects();  
        }

        private void OnBattleActionChanged(BattleActionContext context)
        {
            _battleActionContext = context;

            BattleTargetingProcessor.SetCurrentSearchType(context.TargetSearchType);

            SetTargets(_battleActionContext.TargetType, _battleActionContext.MaxTargetCount);
        }

        private void SetTargets(TargetType targetType, int maxTargetCount) 
        {
            _mainTarget = BattleStateMachine.BattleController.BattleCharactersOnScene.GetCharacterOnScene(targetType, _currentTargetIndex);

            BattleTargetingProcessor.GetSelectedTargets(
                BattleStateMachine.BattleController.BattleCharactersOnScene.GetCharactersByType(targetType),
                _mainTarget,
                maxTargetCount
                );
        }

        private void BattleActionHandler()
        {
            if (BattleTargetingProcessor.AddSelectedTargets(
                ref _currentTargets))
            {
                BattleStateMachine.BattleController.BattleActionController.ExecuteBattleAction(_currentTargets.ToList());

                BattleStateMachine.ChangeState(BattleStateMachine.BattleTargetingState);
            }
        }

        private void CheckCancelKeyPressed()
        {
            if (CancelKeyPressed)
            {

            }
        }
    }
}
