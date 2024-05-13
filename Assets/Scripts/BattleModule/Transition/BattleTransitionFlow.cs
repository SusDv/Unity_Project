using BattleModule.Utility;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Utility;
using VContainer.Unity;

namespace BattleModule.Transition
{
    public class BattleTransitionFlow : IStartable
    {
        private readonly LoadingService _loadingService;
        
        private readonly AssetLoader _assetLoader;

        private BattleTransitionFlow(LoadingService loadingService,
            AssetLoader assetLoader)
        {
            _loadingService = loadingService;
            
            _assetLoader = assetLoader;
        }

        public async void Start()
        {
            await SceneManager.LoadSceneAsync("BattleScene");
        }
    }
}