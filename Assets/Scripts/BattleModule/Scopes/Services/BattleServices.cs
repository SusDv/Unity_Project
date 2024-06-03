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
        [SerializeField] private BattleSpawner _battleSpawner;
        
        [SerializeField] private BattleInput _battleInput;
        
        public void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_battleSpawner);
            
            builder.RegisterComponent(_battleInput);


            builder.Register<BattleCancelableController>(Lifetime.Scoped);
            
            builder.Register<BattleTimerController>(Lifetime.Scoped);
            
            builder.Register<BattleTurnEvents>(Lifetime.Scoped);
            
            builder.Register<BattleTurnController>(Lifetime.Scoped);
            
            builder.Register<BattleTargetingController>(Lifetime.Scoped);
            
            builder.Register<BattleActionController>(Lifetime.Scoped);

            builder.Register<BattleAccuracyController>(Lifetime.Scoped);

            builder.Register<BattleCamera>(Lifetime.Scoped);
            
            builder.Register<BattleController>(Lifetime.Scoped);
        }
    }
}