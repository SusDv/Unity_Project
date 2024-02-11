using BattleModule.Input;
using BattleModule.UI;
using BattleModule.UI.Presenter;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Controllers.Base
{
    public class BattleEntryPoint : LifetimeScope
    {
        [SerializeField] private BattleSpawner _battleSpawner;
        [SerializeField] private Camera _mainCamera;
        
        protected override void Configure(IContainerBuilder builder)
        {
            _battleSpawner.SpawnCharacters();
            
            builder.RegisterComponentInHierarchy<BattleSpawner>();
            builder.RegisterComponentInHierarchy<BattleInput>();
            builder.RegisterComponentInHierarchy<BattleUIController>();

            builder.Register<BattleCamera>(Lifetime.Singleton).WithParameter(_mainCamera);
            
            builder.Register<BattleTurnController>(Lifetime.Singleton);
            
            builder.Register<BattleTargetingController>(Lifetime.Singleton);
            
            builder.Register<BattleActionController>(Lifetime.Singleton);

            builder.RegisterEntryPoint<BattleFightController>();
        }
    }
}