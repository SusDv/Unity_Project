using BattleModule.ActionCore;
using BattleModule.ActionCore.Events;
using BattleModule.StateMachineBase.States.Core;
using BattleModule.Utility.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleModule.StateMachineBase.States
{
    public class BattleTargetingState : BattleState
    {
        private Dictionary<TargetType, Func<Type, Type, bool>> _targetedCharacters;
        public BattleTargetingState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
            _targetedCharacters = new Dictionary<TargetType, Func<Type, Type, bool>>
            {
                { TargetType.ALLY, (selectedCharacterType, characterInTurnType) => selectedCharacterType.Equals(characterInTurnType)},
                { TargetType.ENEMY, (selectedCharacterType, characterInTurnType) => !selectedCharacterType.Equals(characterInTurnType)}
            };
        }

        public override void OnEnter()
        {
            BattleGlobalActionEvent.OnBattleAction += BattleActionHandler;
            AutoSelectEnemy();
            base.OnEnter();
        }
        public override void OnExit()
        {
            BattleGlobalActionEvent.OnBattleAction -= BattleActionHandler;
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

            selectedCharacter = SelectCharacterUsingKeys();

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

        private void AutoSelectEnemy()
        {
            _battleStateMachine.BattleController.Data.SelectedCharacter = _battleStateMachine.BattleController.BattleCharactersOnScene
                .GetMiddleTargetOnScene(
                _battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn().GetType(), 
                    _targetedCharacters[BattleGlobalActionEvent.BattleAction.GetTargetType()]);
        }
        private Character SelectCharacterUsingKeys()
        {
            if (_arrowKeysInput == 0)
            {
                return _battleStateMachine.BattleController.Data.SelectedCharacter;
            }

            return _battleStateMachine.BattleController.BattleCharactersOnScene.GetNearbyCharacter(_battleStateMachine.BattleController.Data.SelectedCharacter, _arrowKeysInput);
        }
        private void BattleActionHandler()
        {
            BattleGlobalActionEvent.BattleAction.PerformAction(_battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn(), _battleStateMachine.BattleController.Data.SelectedCharacter);

            BattleGlobalActionEvent.AdvanceTurn();

            _battleStateMachine.ChangeState(_battleStateMachine.BattleIdleState);
        }
        private void CheckCancelKeyPressed() 
        {
            if (_cancelKeyPressed)
            {
                BattleGlobalActionEvent.BattleAction = BattleDefaultAction.GetBattleDefaultActionInstance();

                _battleStateMachine.ChangeState(_battleStateMachine.BattleIdleState);
            }
        }
    }
}
