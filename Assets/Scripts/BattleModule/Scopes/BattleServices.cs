using BattleModule.Controllers;
using BattleModule.Controllers.Base;
using BattleModule.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Scopes
{
    public class BattleServices : MonoBehaviour
    {
        public static void Configure(IContainerBuilder builder)
        {
            builder.Register<Factory>(Lifetime.Singleton);
            
            builder.Register<BattleTurnController>(Lifetime.Singleton);
            
            builder.Register<BattleTargetingController>(Lifetime.Singleton);
            
            builder.Register<BattleActionController>(Lifetime.Singleton);

            
            builder.RegisterEntryPoint<BattleController>();
        }
    }
}