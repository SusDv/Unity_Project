using BattleModule.Controllers.Modules;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Scopes
{
    public class BattleFlow : IStartable
    {
        private readonly BattleAccuracyController _battleAccuracyController;

        [Inject]
        private BattleFlow(BattleAccuracyController battleAccuracyController)
        {
            _battleAccuracyController = battleAccuracyController;
        }

        public void Start()
        {
            
        }
    }
}