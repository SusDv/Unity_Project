using VContainer;
using VContainer.Unity;

namespace BattleModule.Scopes
{
    public class BattleUIScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            
        }
        
        private void OnValidate()
        {
            if (autoRun)
            {
                autoRun = false;
            }
        }
    }
}