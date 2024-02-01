using BattleModule.Input;
using BattleModule.States.StateMachine;
using Cinemachine;
using UnityEngine;

namespace BattleModule.Controllers.Base
{
    public class BattleFightController : MonoBehaviour
    {
        private BattleStateMachine _battleStateMachine;

        public BattleCamera BattleCamera;

        public BattleInput BattleInput;

        public BattleActionController BattleActionController;

        public BattleTargetingController BattleTargetingController;     

        public BattleTurnController BattleTurnController;

        [SerializeField]
        private LayerMask _characterLayerMask;
        
        [SerializeField]
        private CinemachineVirtualCamera _playersPerspectiveCamera;
        
        [SerializeField]
        private CinemachineVirtualCamera _playersAllyPerspectiveCamera;
        
        public void Init() 
        {
            _battleStateMachine = new BattleStateMachine(this);

            BattleCamera = new BattleCamera(_playersPerspectiveCamera,
                _playersAllyPerspectiveCamera,
                Camera.main, _characterLayerMask);

            BattleTurnController = new BattleTurnController();

            BattleTargetingController = new BattleTargetingController(BattleTurnController);

            BattleActionController = new BattleActionController(BattleTurnController);

        }

        private void Start()
        {
            _battleStateMachine.ChangeState(_battleStateMachine.BattlePlayerActionState);
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
