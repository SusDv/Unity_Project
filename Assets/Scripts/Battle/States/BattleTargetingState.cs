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

        private Stack<Character> _currentTargets;

        private int _currentTargetIndex;

        public BattleTargetingState(BattleStateMachine battleStateMachine) 
            : base(battleStateMachine)
        {}

        public override void OnEnter()
        {
            _currentTargets = new Stack<Character>();

            _data = _battleStateMachine.BattleController.Data;

            StartTurn();

            SelectCharacters();

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
            
            SelectCharacters();
        }

        private void SetupBattleEvents()
        {
            _data.OnBattleActionChanged += SelectCharacters;

            BattleGlobalEventManager.Instance.OnBattleAction += BattleActionHandler;
        }

        private void ClearBattleEvents() 
        {
            _data.OnBattleActionChanged -= SelectCharacters;

            BattleGlobalEventManager.Instance.OnBattleAction -= BattleActionHandler;
        }
        
        private void StartTurn()
        {
            _currentTargetIndex = -1;

            _battleStateMachine.BattleController.BattleCharactersInTurn.OnTurnStarted();

            _data.CharacterInTurn = _battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn();
        }

        private void SelectCharacters()
        {
            _currentBattleActionContext = _data.BattleAction.GetBattleActionContext();

            _mainTarget = _battleStateMachine.BattleController.BattleCharactersOnScene.GetCharacterOnScene(_battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn().GetType(), _currentBattleActionContext.TargetType, _currentTargetIndex);

            BattleTargetingProcessor.GetSelectedTargets(
                _currentBattleActionContext.TargetSearchType,
                _battleStateMachine.BattleController.BattleCharactersOnScene.GetCharactersByType(
                    _battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn().GetType(), _currentBattleActionContext.TargetType),
                _mainTarget,
                _currentBattleActionContext.MaxTargetsCount
                );

            _battleStateMachine.BattleController.OnCharacterTargetChanged?.Invoke(_mainTarget.gameObject.transform.position);
        }

        private void BattleActionHandler()
        {
            if (BattleTargetingProcessor.AddSelectedTargets(
                _currentBattleActionContext.TargetSearchType,
                ref _currentTargets))
            {
                _data.BattleAction.PerformAction(_battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn().GetCharacterStats(), _currentTargets.ToList());

                BattleGlobalEventManager.Instance.AdvanceTurn();

                _battleStateMachine.ChangeState(_battleStateMachine.BattleTargetingState);
            }
        }
        private void CheckCancelKeyPressed()
        {
            if (_cancelKeyPressed)
            {
                BattleTargetingProcessor.OnCancelAction(
                    _currentBattleActionContext.TargetSearchType,
                    ref _currentTargets);
            }
        }
    }
}
