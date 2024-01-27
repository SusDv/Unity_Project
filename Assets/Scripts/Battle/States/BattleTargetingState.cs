using System.Collections.Generic;
using System.Linq;
using BattleModule.ActionCore.Context;
using BattleModule.ActionCore.Events;
using BattleModule.Controllers.Targeting;
using BattleModule.StateMachineBase.States.Core;

namespace BattleModule.StateMachineBase.States
{
    public class BattleTargetingState : BattleState
    {
        private Character _mainTarget;

        private Stack<Character> _currentTargets;

        private int _currentTargetIndex;

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
        }

        private void SetupBattleEvents()
        {
            BattleGlobalEventManager.Instance.OnBattleAction += BattleActionHandler;
        }

        private void ClearBattleEvents() 
        {
            BattleGlobalEventManager.Instance.OnBattleAction -= BattleActionHandler;
        }
        
        private void StartTurn()
        {
            _currentTargetIndex = -1;

            _battleStateMachine.BattleController.BattleCharactersInTurn.OnTurnStarted();  
        }

        private void OnBattleActionChanged(BattleActionContext context)
        {
            BattleTargetingProcessor.SetCurrentSearchType(context.TargetSearchType);

            _mainTarget = _battleStateMachine.BattleController.BattleCharactersOnScene.GetCharacterOnScene(_battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn().GetType(), context.TargetType, _currentTargetIndex);

            BattleTargetingProcessor.GetSelectedTargets(
                _battleStateMachine.BattleController.BattleCharactersOnScene.GetCharactersByType(
                    _battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn().GetType(), context.TargetType),
                _mainTarget,
                context.MaxTargetsCount
                );

            _battleStateMachine.BattleController.OnCharacterTargetChanged?.Invoke(_mainTarget.gameObject.transform.position);
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
