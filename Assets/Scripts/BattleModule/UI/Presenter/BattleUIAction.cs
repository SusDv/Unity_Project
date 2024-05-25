using BattleModule.Actions.BattleActions.Context;
using BattleModule.Controllers.Modules;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.UI.Presenter.SceneSettings.Action;
using BattleModule.UI.View;
using BattleModule.Utility;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility.Constants;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIAction : MonoBehaviour, ILoadingUnit
    {
        [SerializeField]
        private BattleActionSceneSettings _battleActionSceneSettings;

        private BattleActionController _battleActionController;
        
        private BattleTurnEvents _battleTurnEvents;

        private AssetLoader _assetLoader;

        private CanvasProvider _canvasProvider;
        
        
        private BattleUIActionView _battleActionView;
        
        [Inject]
        private void Init(AssetLoader assetLoader,
            CanvasProvider canvasProvider,
            BattleActionController battleActionController,
            BattleTurnEvents battleTurnEvents)
        {
            _assetLoader = assetLoader;

            _canvasProvider = canvasProvider;
            
            _battleActionController = battleActionController;

            _battleTurnEvents = battleTurnEvents;
        }

        public UniTask Load()
        {
            _battleActionView = Instantiate(_assetLoader.GetLoadedAsset<BattleUIActionView>(RuntimeConstants.AssetsName.ActionView), _canvasProvider.Canvas.transform);

            _battleActionController.OnBattleActionChanged += OnBattleActionChanged;

            _battleActionSceneSettings.BattleActionButton.OnButtonClick += _battleTurnEvents.InvokeAction;
            
            return UniTask.CompletedTask;
        }

        private void OnBattleActionChanged(BattleActionContext context) 
        {
            _battleActionView.SetData($"<b><u>Action:</b></u> {context.ObjectInformation.ObjectInformation.Name}");
        }
    }
}
