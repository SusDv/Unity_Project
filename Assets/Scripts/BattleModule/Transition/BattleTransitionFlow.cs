using BattleModule.Utility;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace BattleModule.Transition
{
    public class BattleTransitionFlow : IStartable
    {
        private readonly LoadingService _loadingService;

        private BattleTransitionFlow(LoadingService loadingService)
        {
            _loadingService = loadingService;
        }

        public void Start()
        {
            SceneManager.LoadScene("BattleScene");
        }
    }
}