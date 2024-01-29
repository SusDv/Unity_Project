using BattleModule.Input;
using BattleModule.StateMachineBase;
using UnityEngine;

namespace BattleModule.Controllers.Core
{
    public class BattleFightController : MonoBehaviour
    {
        private BattleStateMachine _battleStateMachine;

        public BattleCamera BattleCamera;

        public BattleInput BattleInput;

        public BattleActionController BattleActionController;

        public BattleCharactersOnScene BattleCharactersOnScene;     

        public BattleTurnController BattleTurnController;

        public LayerMask CharacterLayerMask;

        public void Init() 
        {
            _battleStateMachine = new BattleStateMachine(this);

            BattleCamera = new BattleCamera(FindObjectOfType<Cinemachine.CinemachineVirtualCamera>());

            BattleTurnController = new BattleTurnController();

            BattleCharactersOnScene = new BattleCharactersOnScene(BattleTurnController);

            BattleActionController = new BattleActionController(BattleTurnController);

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
    }
}
