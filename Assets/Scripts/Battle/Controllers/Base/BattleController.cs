using System;
using System.Collections.Generic;
using BattleModule.ActionCore.Events;
using BattleModule.Data;
using BattleModule.Input;
using BattleModule.StateMachineBase;
using UnityEngine;

namespace BattleModule.Controllers.Core
{
    public class BattleController : MonoBehaviour
    {
        private BattleStateMachine _battleStateMachine;

        public BattleCamera BattleCamera;

        public BattleInput BattleInput;

        public BattleStatesData Data;

        public BattleCharactersOnScene BattleCharactersOnScene;

        public BattleCharacterSpawner BattleCharacterSpawner;

        public BattleCharactersInTurn BattleCharactersInTurn;

        public LayerMask CharacterLayerMask;

        public Action<Vector3> OnCharacterTargetChanged;

        public Action OnBattleAction;

        public List<Character> PlayerCharacters;
        public List<Character> EnemyCharacters;

        private void Awake()
        {
            _battleStateMachine = new BattleStateMachine(this);

            BattleInput = GetComponent<BattleInput>();

            BattleCharactersOnScene = new BattleCharactersOnScene();

            Data = new BattleStatesData();

            BattleCamera = new BattleCamera(FindObjectOfType<Cinemachine.CinemachineVirtualCamera>());

            BattleStart();
        }
        private void Start()
        {
            _battleStateMachine.ChangeState(_battleStateMachine.BattleIdleState);
        }
        private void Update()
        {
            _battleStateMachine.OnUpdate();
        }
        private void FixedUpdate()
        {
            _battleStateMachine.OnFixedUpdate();
        }

        private void BattleStart()
        {
            BattleCharactersOnScene.AddCharactersOnScene(BattleCharacterSpawner.SpawnCharacters(PlayerCharacters));
            BattleCharactersOnScene.AddCharactersOnScene(BattleCharacterSpawner.SpawnCharacters(EnemyCharacters));

            BattleCharactersInTurn = new BattleCharactersInTurn(BattleCharactersOnScene.GetCharactersOnScene());

            BattleGlobalActionEventProcessor.SetMaximumTurnsInCycle(BattleCharactersInTurn.GetCharactersInTurn().Count);
        }

        private void OnDestroy()
        {
            OnCharacterTargetChanged = null;
        }
        public Camera GetBattleCamera()
        {
            return Camera.main;
        }
    }
}
