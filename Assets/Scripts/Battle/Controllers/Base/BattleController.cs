using System;
using BattleModule.ActionCore.Events;
using BattleModule.Input;
using BattleModule.StateMachineBase;
using UnityEngine;
using Utils;

namespace BattleModule.Controllers.Core
{
    public class BattleController : MonoBehaviour
    {
        private BattleStateMachine _battleStateMachine;

        [SerializeField]
        private BattleCharactersSpawner _battleCharacterSpawner;

        public BattleCamera BattleCamera;

        public BattleInput BattleInput;

        public BattleActionController BattleActionController;

        public BattleCharactersOnScene BattleCharactersOnScene;     

        public BattleCharactersInTurn BattleCharactersInTurn;

        public LayerMask CharacterLayerMask;

        public Action<Vector3> OnCharacterTargetChanged;

        private void Awake()
        {
            _battleStateMachine = new BattleStateMachine(this);

            BattleInput = GetComponent<BattleInput>();

            BattleCamera = new BattleCamera(FindObjectOfType<Cinemachine.CinemachineVirtualCamera>());

            _battleCharacterSpawner.SpawnCharacters();

            BattleStart();
        }
        private void Start()
        {
            _battleStateMachine.ChangeState(_battleStateMachine.BattleTargetingState);
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
            BattleCharactersOnScene = new BattleCharactersOnScene(_battleCharacterSpawner.GetSpawnedCharacters());

            BattleCharactersInTurn = new BattleCharactersInTurn(_battleCharacterSpawner.GetSpawnedCharacters());

            BattleActionController = new BattleActionController(ref BattleCharactersInTurn.OnCharacterInTurnChanged);
            
            BattleGlobalEventManager.Instance.SetMaximumTurnsInCycle(BattleManager.Instance.CharactersToSpawn.Count);
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
