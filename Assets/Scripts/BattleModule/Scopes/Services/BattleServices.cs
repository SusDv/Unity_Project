using BattleModule.Actions;
using BattleModule.Animation;
using BattleModule.Controllers.Modules;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Scopes.Services
{
    public class BattleServices : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        
        public void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<BattleSpawner>();
            
            builder.RegisterComponentInHierarchy<BattleInput>();

            builder.RegisterComponentInHierarchy<BattleAnimationManager>();


            builder.Register<BattleEventManager>(Lifetime.Singleton);
            
            builder.Register<BattleTurnController>(Lifetime.Singleton);
            
            builder.Register<BattleTargetingController>(Lifetime.Singleton);
            
            builder.Register<BattleActionController>(Lifetime.Singleton);
            
            builder.Register<BattleCamera>(Lifetime.Singleton).WithParameter(_mainCamera);
        }
    }
}