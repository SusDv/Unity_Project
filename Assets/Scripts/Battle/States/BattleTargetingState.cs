using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Battle.Controllers;
using BattleModule.ActionCore.Context;
using BattleModule.ActionCore.Events;
using BattleModule.Data;
using BattleModule.StateMachineBase.States.Core;
using BattleModule.Utility.Enums;

namespace BattleModule.StateMachineBase.States
{
    public class BattleTargetingState : BattleState
    {
        private BattleStatesData _data;

        private int _currentTargetIndex;

        private Character _mainTarget;

        private BattleActionContext _currentBattleActionContext;

        private Stack<Character> _currentTargets = new Stack<Character>();

        private Dictionary<TargetSearchType, BattleTargeting> _targeting;

        public BattleTargetingState(BattleStateMachine battleStateMachine) 
            : base(battleStateMachine)
        {}

        public override void OnEnter()
        {
            _data = _battleStateMachine.BattleController.Data;

            SetupBattleEvents();

            SetupTargetingState();

            BattleActionChanged();

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

        private void BattleActionChanged()
        {
            _currentBattleActionContext = _data.BattleAction.GetBattleActionContext();
        }

        private void SelectCharacters() 
        {
            _targeting[_currentBattleActionContext.TargetSearchType].GetSelectedTargets(
                _battleStateMachine.BattleController.BattleCharactersOnScene.GetCharactersByType(
                    _data.CharacterInTurn.GetType(), _currentBattleActionContext.TargetType),
                _mainTarget,
                _currentBattleActionContext.TargetsToSelect
                );

            _battleStateMachine.BattleController.OnCharacterTargetChanged?.Invoke(_mainTarget.gameObject.transform.position);
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
            _targeting[_currentBattleActionContext.TargetSearchType].AddSelectedTargets(ref _currentTargets);

            if (_currentBattleActionContext.MaxTargetsCount 
                - _currentTargets.Count == 0)
            {
                _data.BattleAction.PerformAction(_battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn(), _currentTargets.ToList());

                BattleGlobalActionEventProcessor.AdvanceTurn();

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
            _data.OnBattleActionChanged += BattleActionChanged;

            BattleGlobalActionEventProcessor.OnBattleAction += BattleActionHandler;
        }

        private void ClearBattleEvents() 
        {
            _data.OnBattleActionChanged -= BattleActionChanged;

            BattleGlobalActionEventProcessor.OnBattleAction -= BattleActionHandler;
        }

        private void SetupTargetingState() 
        {
            _currentTargetIndex = -1;

            _data.CharacterInTurn = _battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn();

            _battleStateMachine.BattleController.BattleCharactersInTurn.OnTurnStarted();
            
            _mainTarget = _battleStateMachine.BattleController.BattleCharactersOnScene.GetInitialTarget(_data.CharacterInTurn.GetType(), _currentBattleActionContext.TargetType);

            SetupBattleTargeting();
        }

        private void SetupBattleTargeting() 
        {
            AOEBattleTargeting aoeBattleTargeting = new AOEBattleTargeting();
            SequenceBattleTargeting sequenceBattleTargeting = new SequenceBattleTargeting();

            _targeting = new Dictionary<TargetSearchType, BattleTargeting>()
            {
                { TargetSearchType.AOE, aoeBattleTargeting },
                { TargetSearchType.SEQUENCE, sequenceBattleTargeting }
            };
        }
    }
}
