using BattleModule.Actions.BattleActions.Context;
using BattleModule.Controllers.Modules;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.UI.Presenter.SceneReferences.Action;
using BattleModule.UI.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIAction : MonoBehaviour, ILoadingUnit
    {
        [SerializeField]
        private BattleActionSceneReference _battleActionSceneReference;

        private BattleActionController _battleActionController;
        
        private BattleTurnEvents _battleTurnEvents;
        
        private BattleUIActionView _battleActionView;
        
        [Inject]
        private void Init(BattleActionController battleActionController,
            BattleTurnEvents battleTurnEvents)
        {
            _battleActionController = battleActionController;

            _battleTurnEvents = battleTurnEvents;
        }

        public UniTask Load()
        {
            _battleActionController.OnBattleActionChanged += OnBattleActionChanged;

            _battleActionSceneReference.BattleActionButton.OnButtonClick += _battleTurnEvents.InvokeAction;
            
            return UniTask.CompletedTask;
        }

        private void OnBattleActionChanged(BattleActionContext context) 
        {
            _battleActionSceneReference.BattleActionButton.SetButtonImage(context.ObjectInformation.Icon);
        }
    }
}
