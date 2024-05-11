using BattleModule.Actions;
using BattleModule.Controllers;
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
        
        [SerializeField] private BattleSpawner _battleSpawner;
        
        [SerializeField] private BattleInput _battleInput;
        
        public void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_battleSpawner);
            
            builder.RegisterComponent(_battleInput);


            builder.Register<BattleEventManager>(Lifetime.Singleton);
            
            builder.Register<BattleTurnController>(Lifetime.Singleton);
            
            builder.Register<BattleTargetingController>(Lifetime.Singleton);
            
            builder.Register<BattleActionController>(Lifetime.Singleton);

            builder.Register<BattleAccuracyController>(Lifetime.Singleton);
            
            builder.Register<BattleCamera>(Lifetime.Singleton).WithParameter(_mainCamera);
            
            builder.Register<BattleController>(Lifetime.Singleton);
        }
    }
}