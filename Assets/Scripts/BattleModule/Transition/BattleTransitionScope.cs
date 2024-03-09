using UnityEngine;
using Utility;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Transition
{
    public class BattleTransitionScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<BattleManager>();
        }
        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(this);
        }
    }
}