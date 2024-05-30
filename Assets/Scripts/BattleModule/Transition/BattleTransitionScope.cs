using BattleModule.Utility;
using UnityEngine;
using Utility;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Transition
{
    public class BattleTransitionScope : LifetimeScope
    {
        [SerializeField] 
        private BattleTransitionData _battleTransitionData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_battleTransitionData);
            
            builder.Register<LoadingService>(Lifetime.Scoped);

            builder.Register<AssetLoader>(Lifetime.Singleton);

            builder.RegisterEntryPoint<BattleTransitionFlow>();
        }
        
        protected override void Awake()
        {
            IsRoot = true;
            
            DontDestroyOnLoad(this);
            
            base.Awake();
        }
    }
}