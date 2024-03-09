using System.Collections.Generic;
using BattleModule.Actions;
using BattleModule.Controllers.Turn;
using BattleModule.Input;
using BattleModule.States.StateMachine;
using CharacterModule;
using JetBrains.Annotations;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Controllers.Base
{
    [UsedImplicitly]
    public class BattleController : ITickable, IFixedTickable 
    {
        private readonly BattleStateMachine _battleStateMachine;

        public readonly BattleInput BattleInput;
        
        public readonly BattleCamera BattleCamera;

        public readonly BattleActionController BattleActionController;

        public readonly BattleTargetingController BattleTargetingController;     

        public readonly BattleTurnController BattleTurnController;
        
        public BattleController(BattleInput battleInput,
            BattleCamera battleCamera, BattleActionController battleActionController,
            BattleTargetingController battleTargetingController, 
            BattleTurnController battleTurnController,
            BattleSpawner battleSpawner)
        {
            _battleStateMachine = new BattleStateMachine(this);
            
            BattleInput = battleInput;
            
            BattleCamera = battleCamera;
            
            BattleActionController = battleActionController;
            
            BattleTargetingController = battleTargetingController;
            
            BattleTurnController = battleTurnController;

            battleSpawner.OnCharactersSpawned += OnCharactersSpawned;
        }

        private void OnCharactersSpawned(List<Character> characters)
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
