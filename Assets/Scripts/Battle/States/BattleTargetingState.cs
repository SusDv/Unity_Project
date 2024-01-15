using System;
using System.Collections.Generic;
using BattleModule.ActionCore.Events;
using BattleModule.StateMachineBase.States.Core;
using BattleModule.Utility.Enums;
using UnityEngine;

namespace BattleModule.StateMachineBase.States
{
    public class BattleTargetingState : BattleState
    {
        private readonly Dictionary<TargetType, Func<Type, Type, bool>> 
            _charactersToTarget
                = new Dictionary<TargetType, Func<Type, Type, bool>>
                {
                    { TargetType.ALLY, 
                        (selectedCharacterType, characterInTurnType) => 
                            selectedCharacterType.Equals(characterInTurnType)},
                    { TargetType.ENEMY, 
                        (selectedCharacterType, characterInTurnType) => 
                        !selectedCharacterType.Equals(characterInTurnType)}
                };

        private int _currentTargetIndex;


        public BattleTargetingState(BattleStateMachine battleStateMachine) 
            : base(battleStateMachine)
        {

        }

        public override void OnEnter()
        {
            _currentTargetIndex = -1;

            _battleStateMachine.BattleController.BattleCharactersInTurn.ResetCharacterInTurnBattlePoints();

            _battleStateMachine.BattleController.BattleCharactersInTurn.TriggerCharacterInTurnTemporaryTurnModifiers();

            BattleGlobalActionEventProcessor.OnBattleAction += BattleActionHandler;

            BattleGlobalActionEventProcessor.OnBattleActionChanged += SelectCharacter;

            SelectCharacter();
            base.OnEnter();
        }
        public override void OnExit()
        {
            BattleGlobalActionEventProcessor.OnBattleAction -= BattleActionHandler;
            
            BattleGlobalActionEventProcessor.OnBattleActionChanged -= SelectCharacter;
            
            base.OnExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            _battleStateMachine.BattleController.Data.SelectedCharacter = SelectEnemyOnScene();
            
            CheckCancelKeyPressed();
        }
        protected Character SelectEnemyOnScene()
        {
            Character selectedCharacter = _battleStateMachine.BattleController.Data.SelectedCharacter;

            (_currentTargetIndex, selectedCharacter) = SelectCharacterUsingKeys();

            if (!_battleStateMachine.BattleController.BattleInput.BattleActions.LeftMouseButton.WasPressedThisFrame())
            {
                _battleStateMachine.BattleController.OnCharacterTargetChanged?.Invoke(selectedCharacter.gameObject.transform.position);
                return selectedCharacter;
            }

            RaycastHit characterHit;

            Ray characterRay = _battleStateMachine.BattleController.GetBattleCamera().ScreenPointToRay(_battleStateMachine.BattleController.BattleInput.MousePosition);

            if (Physics.Raycast(characterRay, out characterHit, _battleStateMachine.BattleController.GetBattleCamera().farClipPlane, _battleStateMachine.BattleController.CharacterLayerMask))
            {
                if (!characterHit.collider.GetComponent<Player>())
                {
                    selectedCharacter = characterHit.collider.GetComponent<Character>();
                }
            }

            _battleStateMachine.BattleController.OnCharacterTargetChanged?.Invoke(selectedCharacter.gameObject.transform.position);

            return selectedCharacter;
        }

        private void SelectCharacter()
        {
            _battleStateMachine.BattleController.Data.SelectedCharacter = _battleStateMachine.BattleController.BattleCharactersOnScene
                .GetCharacterOnScene(
                _battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn(), 
                    _charactersToTarget[BattleGlobalActionEventProcessor.BattleAction.GetBattleActionContext().TargetType], _currentTargetIndex);
        }
        private (int, Character) SelectCharacterUsingKeys()
        {
            if (_arrowKeysInput == 0)
            {
                return (_currentTargetIndex, _battleStateMachine.BattleController.Data.SelectedCharacter);
            }

            return _battleStateMachine.BattleController.BattleCharactersOnScene.GetNearbyCharacterOnScene(_battleStateMachine.BattleController.Data.SelectedCharacter, _arrowKeysInput);
        }
        private void BattleActionHandler()
        {
            BattleGlobalActionEventProcessor.BattleAction.PerformAction(_battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn(), _battleStateMachine.BattleController.Data.SelectedCharacter);

            BattleGlobalActionEventProcessor.AdvanceTurn();

            _battleStateMachine.ChangeState(_battleStateMachine.BattleTargetingState);
        }

        private void CheckCancelKeyPressed() 
        {
            if (_cancelKeyPressed)
            {
                
            }
        }
    }
}
