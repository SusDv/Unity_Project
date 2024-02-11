using BattleModule.Input;
using BattleModule.States.StateMachine;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Controllers.Base
{
    public class BattleFightController : IInitializable, ITickable, IFixedTickable 
    {
        private readonly BattleStateMachine _battleStateMachine;

        public readonly BattleInput BattleInput;
        
        public readonly BattleCamera BattleCamera;

        public readonly BattleActionController BattleActionController;

        public readonly BattleTargetingController BattleTargetingController;     

        public readonly BattleTurnController BattleTurnController;

        [Inject]
        public BattleFightController(BattleInput battleInput,
            BattleCamera battleCamera, BattleActionController battleActionController,
            BattleTargetingController battleTargetingController, BattleTurnController battleTurnController)
        {
            _battleStateMachine = new BattleStateMachine(this);
            
            BattleInput = battleInput;
            
            BattleCamera = battleCamera;
            
            BattleActionController = battleActionController;
            
            BattleTargetingController = battleTargetingController;
            
            BattleTurnController = battleTurnController;
        }
        
        public void Initialize()
        {
            _battleStateMachine.ChangeState(_battleStateMachine.BattlePlayerActionState);
        }

        public void Tick()
        {
            _battleStateMachine.OnUpdate();
        }

        public void FixedTick()
        {
            _battleStateMachine.OnFixedUpdate();
        }
    }
}
