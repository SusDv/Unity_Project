using BattleModule.Input;
using BattleModule.States.StateMachine;
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
        public void Init() 
        {
            _battleStateMachine = new BattleStateMachine(this);

            BattleCamera = new BattleCamera(FindObjectOfType<Cinemachine.CinemachineVirtualCamera>());

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
