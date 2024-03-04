using BattleModule.Actions;
using BattleModule.Controllers.Turn;
using BattleModule.Input;
using BattleModule.States.StateMachine;
using JetBrains.Annotations;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Controllers.Base
{
    [UsedImplicitly]
    public class BattleController : IInitializable, ITickable, IFixedTickable 
    {
        private readonly BattleStateMachine _battleStateMachine;

        public readonly BattleInput BattleInput;
        
        public readonly BattleCamera BattleCamera;

        public readonly BattleActionController BattleActionController;

        public readonly BattleTargetingController BattleTargetingController;     

        public readonly BattleTurnController BattleTurnController;
        
        public BattleController(BattleInput battleInput,
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
