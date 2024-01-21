using System.Collections.Generic;
using System.Linq;
using BattleModule.ActionCore.Context;
using BattleModule.ActionCore.Events;
using BattleModule.Controllers.Targeting;
using BattleModule.Data;
using BattleModule.StateMachineBase.States.Core;
using BattleModule.Utility.Enums;

namespace BattleModule.StateMachineBase.States
{
    public class BattleTargetingState : BattleState
    {
        private BattleStatesData _data;

        private BattleActionContext _currentBattleActionContext;


        private Character _mainTarget;

        private Stack<Character> _currentTargets = new Stack<Character>();

        private int _currentTargetIndex;

        public BattleTargetingState(BattleStateMachine battleStateMachine) 
            : base(battleStateMachine)
        {}

        public override void OnEnter()
        {
            _data = _battleStateMachine.BattleController.Data;

            SetupBattleEvents();

            StartTurn();

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

            ChangeTargetWithInput();

            CheckCancelKeyPressed();
        }

        private void ChangeTargetWithInput()
        {
            SelectCharacterUsingKeys();
        }

        private void SetBattleActionContext()
        {
            _currentBattleActionContext = _data.BattleAction.GetBattleActionContext();
        }

        private void SelectCharacterUsingKeys()
        {
            if (_arrowKeysInput == 0)
            {
                return;
            }

            (_currentTargetIndex, _mainTarget) = _battleStateMachine.BattleController.BattleCharactersOnScene.GetNearbyCharacterOnScene(_mainTarget, _arrowKeysInput);
            
            SelectCharacters();
        }

        private void BattleActionHandler()
        {
            BattleTargetingProcessor.AddSelectedTargets(
                _currentBattleActionContext.TargetSearchType,
                ref _currentTargets);

            if (_currentBattleActionContext.MaxTargetsCount 
                - _currentTargets.Count == 0)
            {
                _data.BattleAction.PerformAction(_battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn(), _currentTargets.ToList());

                BattleGlobalEventManager.Instance.AdvanceTurn();

                _battleStateMachine.ChangeState(_battleStateMachine.BattleTargetingState);
            }
        }

        private void CheckCancelKeyPressed() 
        {
            if (_cancelKeyPressed)
            {
                if (_currentBattleActionContext.TargetSearchType
                    == TargetSearchType.SEQUENCE) 
                {
                    _currentTargets.Pop();
                }
            }
        }

        private void SetupBattleEvents()
        {
            _data.OnBattleActionChanged += SetBattleActionContext;

            BattleGlobalEventManager.Instance.OnBattleAction += BattleActionHandler;
        }

        private void ClearBattleEvents() 
        {
            _data.OnBattleActionChanged -= SetBattleActionContext;

            BattleGlobalEventManager.Instance.OnBattleAction -= BattleActionHandler;
        }
        private void StartTurn()
        {
            _battleStateMachine.BattleController.BattleCharactersInTurn.OnTurnStarted();

            _data.CharacterInTurn = _battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn();

            SetupTargeting();

            SelectCharacters();
        }

        private void SelectCharacters()
        {
            BattleTargetingProcessor.GetSelectedTargets(
                _currentBattleActionContext.TargetSearchType,
                _battleStateMachine.BattleController.BattleCharactersOnScene.GetCharactersByType(
                    _data.CharacterInTurn.GetType(), _currentBattleActionContext.TargetType),
                _mainTarget,
                _currentBattleActionContext.MaxTargetsCount
                );

            _battleStateMachine.BattleController.OnCharacterTargetChanged?.Invoke(_mainTarget.gameObject.transform.position);
        }

        private void SetupTargeting() 
        {
            SetBattleActionContext();

            _currentTargetIndex = -1;

            _mainTarget = _battleStateMachine.BattleController.BattleCharactersOnScene.GetInitialTarget(_data.CharacterInTurn.GetType(), _currentBattleActionContext.TargetType);

            _battleStateMachine.BattleController.OnCharacterTargetChanged?.Invoke(_mainTarget.gameObject.transform.position);
        }
    }
}
