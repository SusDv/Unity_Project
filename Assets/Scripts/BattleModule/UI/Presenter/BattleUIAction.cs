using BattleModule.Actions.Context;
using BattleModule.Controllers.Modules;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.UI.Presenter.SceneReferences.Action;
using BattleModule.UI.View;
using BattleModule.Utility.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIAction : MonoBehaviour, ILoadingUnit, IUIElement
    {
        [SerializeField]
        private BattleActionSceneReference _battleActionSceneReference;

        private BattleActionController _battleActionController;

        private BattleUIController _battleUIController;
        
        private BattleTurnEvents _battleTurnEvents;
        
        private BattleUIActionView _battleActionView;
        
        [Inject]
        private void Init(BattleActionController battleActionController,
            BattleUIController battleUIController,
            BattleTurnEvents battleTurnEvents)
        {
            _battleActionController = battleActionController;

            _battleUIController = battleUIController;
            
            _battleTurnEvents = battleTurnEvents;
        }

        public UniTask Load()
        {
            _battleActionController.OnBattleActionChanged += OnBattleActionChanged;

            _battleActionSceneReference.BattleActionButton.OnButtonClick += _battleTurnEvents.InvokeAction;

            _battleUIController.AddAsUIElement(this);
            
            return UniTask.CompletedTask;
        }

        public void ToggleVisibility()
        {
            _battleActionSceneReference.BattleActionButton.ToggleVisibility();
        }

        private void OnBattleActionChanged(BattleActionContext context) 
        {
            _battleActionSceneReference.BattleActionButton.SetButtonImage(context.ObjectInformation.Icon);
        }
    }
}
