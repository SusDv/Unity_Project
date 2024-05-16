using BattleModule.UI.Presenter.SceneSettings.Inventory;
using BattleModule.UI.View;
using BattleModule.Utility;
using CharacterModule.Inventory.Items.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIItemDescription : MonoBehaviour, ILoadingUnit
    {
        [SerializeField]
        private BattleItemDescriptionSceneSettings _battleItemDescriptionSceneSettings;

        private AssetLoader _assetLoader;
        
        private BattleUIItemDescriptionView _battleUIItemDescriptionView;
        
        [Inject]
        private void Init(AssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        public UniTask Load()
        {
            _battleUIItemDescriptionView = Instantiate(_assetLoader.GetLoadedAsset<BattleUIItemDescriptionView>(RuntimeConstants.AssetsName.ItemDescriptionView),
               _battleItemDescriptionSceneSettings.BattleItemDescriptionWindow.transform);
           
            return UniTask.CompletedTask;
        }

        public void SetDescriptionPanelVisibility(bool inventoryVisibilityStatus)
        {
            _battleItemDescriptionSceneSettings.BattleItemDescriptionWindow.SetActive(inventoryVisibilityStatus && _battleItemDescriptionSceneSettings.BattleItemDescriptionWindow.activeSelf);
        }

        public void SetItemDescription(ItemBase itemBase)
        {
            _battleItemDescriptionSceneSettings.BattleItemDescriptionWindow.SetActive(!_battleItemDescriptionSceneSettings.BattleItemDescriptionWindow.activeSelf);
            
            if (_battleItemDescriptionSceneSettings.BattleItemDescriptionWindow.activeSelf)
            {
                _battleUIItemDescriptionView.SetData(itemBase.ObjectInformation);
            }
        }
    }
}