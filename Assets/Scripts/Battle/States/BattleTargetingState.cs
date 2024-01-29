using System.Collections.Generic;
using System.Linq;
using BattleModule.ActionCore.Context;
using BattleModule.ActionCore.Events;
using BattleModule.Controllers.Targeting;
using BattleModule.StateMachineBase.States.Core;
using BattleModule.Utility.Enums;

namespace BattleModule.StateMachineBase.States
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

            _battleStateMachine.BattleController.BattleActionController.OnBattleActionChanged += OnBattleActionChanged;

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
            if (_arrowKeysInput == 0)
            {
                return;
            }

            _currentTargetIndex = _battleStateMachine.BattleController.BattleCharactersOnScene.GetNearbyCharacterOnSceneIndex(_mainTarget, _arrowKeysInput);

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

            _battleStateMachine.BattleController.BattleTurnController.TurnEffects();  
        }

        private void OnBattleActionChanged(BattleActionContext context)
        {
            _battleActionContext = context;

            BattleTargetingProcessor.SetCurrentSearchType(context.TargetSearchType);

            SetTargets(_battleActionContext.TargetType, _battleActionContext.MaxTargetCount);
        }

        private void SetTargets(TargetType targetType, int maxTargetCount) 
        {
            _mainTarget = _battleStateMachine.BattleController.BattleCharactersOnScene.GetCharacterOnScene(targetType, _currentTargetIndex);

            BattleTargetingProcessor.GetSelectedTargets(
                _battleStateMachine.BattleController.BattleCharactersOnScene.GetCharactersByType(targetType),
                _mainTarget,
                maxTargetCount
                );
        }

        private void BattleActionHandler()
        {
            if (BattleTargetingProcessor.AddSelectedTargets(
                ref _currentTargets))
            {
                _battleStateMachine.BattleController.BattleActionController.ExecuteBattleAction(_currentTargets.ToList());

                _battleStateMachine.ChangeState(_battleStateMachine.BattleTargetingState);
            }
        }

        private void CheckCancelKeyPressed()
        {
            if (_cancelKeyPressed)
            {

            }
        }
    }
}
