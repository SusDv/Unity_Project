using BattleModule.UI.Presenter;
using BattleModule.Utility;
using UnityEngine;
using Utility;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Scopes.Services
{
    public class BattleUIServices : MonoBehaviour
    {
        [SerializeField] private CanvasProvider _canvasProvider;
        [SerializeField] private UILoadingScreen _uiLoadingScreen;
        
        [SerializeField] private BattleUIAccuracy _battleUIAccuracy;
        [SerializeField] private BattleUIAction _battleUIAction;
        [SerializeField] private BattleUIInventory _battleUIInventory;
        [SerializeField] private BattleUIItemDescription _battleUIItemDescription;
        [SerializeField] private BattleUIEnemy _battleUIEnemy;
        [SerializeField] private BattleUIPlayer _battleUIPlayer;
        [SerializeField] private BattleUISpells _battleUISpells;
        [SerializeField] private BattleUITargeting _battleUITargeting;
        [SerializeField] private BattleUITurn _battleUITurn;
            
        public void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_canvasProvider);
            builder.RegisterComponent(_uiLoadingScreen);
            
            builder.RegisterComponent(_battleUIAccuracy);
            builder.RegisterComponent(_battleUIAction);
            builder.RegisterComponent(_battleUIInventory);
            builder.RegisterComponent(_battleUIItemDescription);
            builder.RegisterComponent(_battleUIEnemy);
            builder.RegisterComponent(_battleUIPlayer);
            builder.RegisterComponent(_battleUISpells);
            builder.RegisterComponent(_battleUITargeting);
            builder.RegisterComponent(_battleUITurn);
        }
    }
}