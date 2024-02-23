using System;
using UnityEngine;
using Utils;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Scopes
{
    public class BattleTransferData : LifetimeScope
    {
        [SerializeField]
        private BattleManager _battleManager;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_battleManager);
        }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
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