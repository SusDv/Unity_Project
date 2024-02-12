using System;
using BattleModule.Controllers;
using BattleModule.Controllers.Base;
using BattleModule.Input;
using BattleModule.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Scopes
{
    public class BattleScope : LifetimeScope
    {
        [SerializeField] private Camera _mainCamera;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<BattleSpawner>();
            
            builder.RegisterComponentInHierarchy<BattleInput>();
            
            BattleServices.Configure(builder);

            builder.Register<BattleCamera>(Lifetime.Singleton).WithParameter(_mainCamera);

            builder.Register<BattleServices>(Lifetime.Singleton).WithParameter(builder);
        }

        private void OnValidate()
        {
            if (autoRun)
            {
                autoRun = false;
            }
        }
    }
}