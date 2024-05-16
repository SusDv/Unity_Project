using BattleModule.Controllers;
using BattleModule.Controllers.Modules;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Input;
using BattleModule.UI.Presenter;
using BattleModule.Utility;
using Cysharp.Threading.Tasks;
using Utility;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Scopes
{
    public class BattleFlow : IStartable
    {
        private readonly LoadingService _loadingService;
        private readonly AssetLoader _assetLoader;
        
        private readonly BattleInput _battleInput;
        private readonly BattleSpawner _battleSpawner;
        private readonly BattleController _battleController;
        private readonly BattleTurnController _battleTurnController;
        private readonly BattleTargetingController _battleTargetingController;
        private readonly BattleActionController _battleActionController;
        private readonly BattleAccuracyController _battleAccuracyController;
        
        private readonly BattleUIInventory _battleUIInventory;
        private readonly BattleUIItemDescription _battleUIItemDescription;
        private readonly BattleUIAccuracy _battleUIAccuracy;
        private readonly BattleUIAction _battleUIAction;
        private readonly BattleUIEnemy _battleUIEnemy;
        private readonly BattleUIPlayer _battleUIPlayer;
        private readonly BattleUISpells _battleUISpells;
        private readonly BattleUITargeting _battleUITargeting;
        private readonly BattleUITurn _battleUITurn;
        
        [Inject]
        private BattleFlow(LoadingService loadingService,
            AssetLoader assetLoader,
            BattleInput battleInput,
            BattleSpawner battleSpawner,
            BattleController battleController,
            BattleTurnController battleTurnController,
            BattleTargetingController battleTargetingController,
            BattleActionController battleActionController,
            BattleAccuracyController battleAccuracyController,
            BattleUIInventory battleUIInventory,
            BattleUIItemDescription battleUIItemDescription,
            BattleUIAccuracy battleUIAccuracy,
            BattleUIAction battleUIAction,
            BattleUIEnemy battleUIEnemy,
            BattleUIPlayer battleUIPlayer,
            BattleUISpells battleUISpells,
            BattleUITargeting battleUITargeting,
            BattleUITurn battleUITurn)
        {
            _loadingService = loadingService;
            _assetLoader = assetLoader;
            
            _battleInput = battleInput;
            _battleSpawner = battleSpawner;
            _battleController = battleController;
            _battleTurnController = battleTurnController;
            _battleTargetingController = battleTargetingController;
            _battleActionController = battleActionController;
            _battleAccuracyController = battleAccuracyController;
            
            _battleUIAccuracy = battleUIAccuracy;
            _battleUIAction = battleUIAction;
            _battleUIInventory = battleUIInventory;
            _battleUIItemDescription = battleUIItemDescription;
            _battleUIEnemy = battleUIEnemy;
            _battleUIPlayer = battleUIPlayer;
            _battleUISpells = battleUISpells;
            _battleUITargeting = battleUITargeting;
            _battleUITurn = battleUITurn;
        }

        public async void Start()
        {
            await LoadBattle();
            
            _battleController.StartBattle();
        }

        private async UniTask LoadBattle()
        {
            await _assetLoader.LoadBattleAssets();

            await _loadingService.BeginLoading(_battleInput);
            await _loadingService.BeginLoading(_battleSpawner);
            await _loadingService.BeginLoading(_battleTurnController, _battleSpawner.GetSpawnedCharacters());
            await _loadingService.BeginLoading(_battleTargetingController, _battleSpawner.GetSpawnedCharacters());
            await _loadingService.BeginLoading(_battleActionController);
            await _loadingService.BeginLoading(_battleAccuracyController);
            
            await _loadingService.BeginLoading(_battleUIInventory);
            await _loadingService.BeginLoading(_battleUIItemDescription);
            await _loadingService.BeginLoading(_battleUIAccuracy, _battleSpawner.GetSpawnedCharacters());
            await _loadingService.BeginLoading(_battleUIAction);
            await _loadingService.BeginLoading(_battleUIEnemy, _battleSpawner.GetSpawnedCharacters());
            await _loadingService.BeginLoading(_battleUIPlayer, _battleSpawner.GetSpawnedCharacters());
            await _loadingService.BeginLoading(_battleUISpells);
            await _loadingService.BeginLoading(_battleUITargeting, _battleSpawner.GetSpawnedCharacters());
            await _loadingService.BeginLoading(_battleUITurn, _battleSpawner.GetSpawnedCharacters());
        }
    }
}