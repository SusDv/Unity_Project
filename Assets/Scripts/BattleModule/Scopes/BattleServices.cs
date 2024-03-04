using BattleModule.Actions;
using BattleModule.Animation;
using BattleModule.Controllers;
using BattleModule.Controllers.Turn;
using BattleModule.Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Scopes
{
    public class BattleServices : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        
        public void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<BattleSpawner>();
            
            builder.RegisterComponentInHierarchy<BattleInput>();
            
            builder.RegisterComponentInHierarchy<BattleEventManager>();

            builder.RegisterComponentInHierarchy<BattleAnimationManager>();
            
            
            builder.Register<BattleTurnController>(Lifetime.Singleton);
            
            builder.Register<BattleTargetingController>(Lifetime.Singleton);
            
            builder.Register<BattleActionController>(Lifetime.Singleton);
            
            builder.Register<BattleCamera>(Lifetime.Singleton).WithParameter(_mainCamera);
        }
    }
}