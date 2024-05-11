using BattleModule.Scopes.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Scopes
{
    public class BattleScope : LifetimeScope
    {
        [SerializeField] private BattleServices _battleServices;
        
        [SerializeField] private BattleUIServices _battleUIServices;
        
        protected override void Configure(IContainerBuilder builder)
        {
            _battleServices.Configure(builder);
            
            _battleUIServices.Configure(builder);
            
            builder.RegisterEntryPoint<BattleFlow>();
        }
    }
}