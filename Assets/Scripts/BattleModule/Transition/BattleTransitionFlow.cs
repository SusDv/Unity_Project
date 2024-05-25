using BattleModule.Utility;
using UnityEngine.SceneManagement;
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

        public void Start()
        {
            SceneManager.LoadScene("BattleScene");
        }
    }
}