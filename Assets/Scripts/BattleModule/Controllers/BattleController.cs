using BattleModule.Actions;
using BattleModule.Controllers.Modules;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Input;
using BattleModule.States.StateMachine;

namespace BattleModule.Controllers
{
    public class BattleController
    {
        private readonly BattleStateMachine _battleStateMachine;

        public readonly BattleInput BattleInput;
        
        public readonly BattleCamera BattleCamera;

        public readonly BattleActionController BattleActionController;

        public readonly BattleTargetingController BattleTargetingController;     

        public readonly BattleTurnController BattleTurnController;

        public readonly BattleEventManager BattleEventManager;
        
        public BattleController(BattleInput battleInput,
            BattleCamera battleCamera, 
            BattleActionController battleActionController,
            BattleTargetingController battleTargetingController, 
            BattleTurnController battleTurnController,
            BattleEventManager battleEventManager)
        {
            _battleStateMachine = new BattleStateMachine(this);
            
            BattleInput = battleInput;
            
            BattleCamera = battleCamera;
            
            BattleActionController = battleActionController;
            
            BattleTargetingController = battleTargetingController;
            
            BattleTurnController = battleTurnController;

            BattleEventManager = battleEventManager;
        }

        public void StartBattle()
        {
            _battleStateMachine.ChangeState(_battleStateMachine.BattlePlayerActionState);
        }
    }
}
