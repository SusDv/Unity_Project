using BattleModule.Controllers;
using BattleModule.Controllers.Modules;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Input;
using BattleModule.UI.Presenter;
using BattleModule.Utility;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Scopes
{
    public class BattleFlow : IStartable
    {
        private readonly LoadingService _loadingService;

        private readonly BattleInput _battleInput;
        
        private readonly BattleSpawner _battleSpawner;

        private readonly BattleController _battleController;

        private readonly BattleTurnController _battleTurnController;

        private readonly BattleTargetingController _battleTargetingController;

        private readonly BattleActionController _battleActionController;
        
        private readonly BattleAccuracyController _battleAccuracyController;

        private readonly BattleUIInventory _battleUIInventory;

        private readonly BattleUIAccuracy _battleUIAccuracy;
        
        [Inject]
        private BattleFlow(LoadingService loadingService,
            BattleInput battleInput,
            BattleSpawner battleSpawner,
            BattleController battleController,
            BattleTurnController battleTurnController,
            BattleTargetingController battleTargetingController,
            BattleActionController battleActionController,
            BattleAccuracyController battleAccuracyController,
            BattleUIInventory battleUIInventory,
            BattleUIAccuracy battleUIAccuracy)
        {
            _loadingService = loadingService;

            _battleInput = battleInput;
            
            _battleSpawner = battleSpawner;
            
            _battleController = battleController;

            _battleTurnController = battleTurnController;

            _battleTargetingController = battleTargetingController;

            _battleActionController = battleActionController;
            
            _battleAccuracyController = battleAccuracyController;

            _battleUIInventory = battleUIInventory;

            _battleUIAccuracy = battleUIAccuracy;
        }

        public async void Start()
        {
            await _loadingService.BeginLoading(_battleInput);
            
            await _loadingService.BeginLoading(_battleSpawner);
            
            await _loadingService.BeginLoading(_battleTurnController, _battleSpawner.GetSpawnedCharacters());

            await _loadingService.BeginLoading(_battleTargetingController);
            
            await _loadingService.BeginLoading(_battleActionController);

            await _loadingService.BeginLoading(_battleAccuracyController);

            await _loadingService.BeginLoading(_battleUIInventory);
            
            await _loadingService.BeginLoading(_battleUIAccuracy, _battleSpawner.GetSpawnedCharacters());
            
            _battleController.StartBattle();
        }
    }
}