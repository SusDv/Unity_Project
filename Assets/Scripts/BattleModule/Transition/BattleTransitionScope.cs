using BattleModule.Scopes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Transition
{
    public class BattleTransitionScope : LifetimeScope
    {
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<BattleTransitionData>(Lifetime.Singleton);
        }

        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(this);
        }
    }
}