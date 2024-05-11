using BattleModule.UI.Presenter;
using BattleModule.UI.Presenter.SceneSettings.Accuracy;
using BattleModule.UI.Presenter.SceneSettings.Action;
using BattleModule.UI.Presenter.SceneSettings.Enemy;
using BattleModule.UI.Presenter.SceneSettings.Inventory;
using BattleModule.UI.Presenter.SceneSettings.Player;
using BattleModule.UI.Presenter.SceneSettings.Spells;
using BattleModule.UI.Presenter.SceneSettings.Targeting;
using BattleModule.UI.Presenter.SceneSettings.Turn;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Scopes.Services
{
    public class BattleUIServices : MonoBehaviour
    {
        [SerializeField] private BattleActionSceneSettings _battleActionSceneSettings;
        [SerializeField] private BattleEnemySceneSettings _battleEnemySceneSettings;

        [SerializeField] private BattleInventorySceneSettings _battleInventorySceneSettings;
        [SerializeField] private BattleItemDescriptionSceneSettings _battleItemDescriptionSceneSettings;
        [SerializeField] private BattlePlayerSceneSettings _battlePlayerSceneSettings;

        [SerializeField] private BattleSpellsSceneSettings _battleSpellsSceneSettings;
        [SerializeField] private BattleTargetingSceneSettings _battleTargetingSceneSettings;
        [SerializeField] private BattleTurnSceneSettings _battleTurnSceneSettings;

        [SerializeField] private BattleAccuracySceneSettings _battleAccuracySceneSettings;
        
        public void Configure(IContainerBuilder builder)
        {
            builder.Register<BattleUIItemDescription>(Lifetime.Singleton);
            
            builder.RegisterComponent(_battleActionSceneSettings);
            
            builder.RegisterComponent(_battleEnemySceneSettings);
            
            builder.RegisterComponent(_battleInventorySceneSettings);
            
            builder.RegisterComponent(_battleItemDescriptionSceneSettings);
            
            builder.RegisterComponent(_battlePlayerSceneSettings);
            
            builder.RegisterComponent(_battleSpellsSceneSettings);
            
            builder.RegisterComponent(_battleTargetingSceneSettings);
            
            builder.RegisterComponent(_battleTurnSceneSettings);

            builder.RegisterComponent(_battleAccuracySceneSettings);

            builder.RegisterComponentInHierarchy<BattleUIInventory>();
            
            builder.RegisterComponentInHierarchy<BattleUIAccuracy>();
        }
    }
}