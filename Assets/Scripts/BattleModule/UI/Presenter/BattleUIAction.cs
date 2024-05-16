using System;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Controllers.Modules;
using BattleModule.UI.Presenter.SceneSettings.Action;
using BattleModule.UI.View;
using BattleModule.Utility;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIAction : MonoBehaviour, ILoadingUnit
    {
        [SerializeField]
        private BattleActionSceneSettings _battleActionSceneSettings;

        private BattleActionController _battleActionController;

        private AssetLoader _assetLoader;

        private CanvasProvider _canvasProvider;
        
        private BattleUIActionView _battleActionView;
        
        [Inject]
        private void Init(AssetLoader assetLoader,
            CanvasProvider canvasProvider,
            BattleActionController battleActionController)
        {
            _assetLoader = assetLoader;

            _canvasProvider = canvasProvider;
            
            _battleActionController = battleActionController;
        }

        public UniTask Load()
        {
            _battleActionView = Instantiate(_assetLoader.GetLoadedAsset<BattleUIActionView>(RuntimeConstants.AssetsName.ActionView), _canvasProvider.Canvas.transform);
            
            SetActionButtonEvent(_battleActionController.GetInvokeAction());
            
            _battleActionController.OnBattleActionChanged += OnBattleActionChanged;
            
            return UniTask.CompletedTask;
        }

        private void SetActionButtonEvent(Action<object> actionEvent)
        {
            _battleActionSceneSettings.BattleActionButton.OnButtonClick += actionEvent;
        }

        private void OnBattleActionChanged(BattleActionContext context) 
        {
            _battleActionView.SetData($"<b><u>Action:</b></u> {context.ObjectInformation.ObjectInformation.Name}");
        }
    }
}
