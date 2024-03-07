using BattleModule.Scopes;
using UnityEngine;
using Utils;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Transition
{
    public class BattleTransitionScope : LifetimeScope
    {
        [SerializeField]
        private BattleManager _battleManager;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<BattleTransitionData>(Lifetime.Singleton).WithParameter(_battleManager);
        }

        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(this);
        }
    }
}