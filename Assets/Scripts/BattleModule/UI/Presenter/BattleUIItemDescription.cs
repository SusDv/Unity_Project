using BattleModule.UI.Presenter.SceneReferences.Inventory;
using BattleModule.UI.View;
using CharacterModule.Inventory.Items.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using Utility.Constants;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIItemDescription : MonoBehaviour, ILoadingUnit
    {
        [SerializeField]
        private BattleItemDescriptionSceneReference _battleItemDescriptionSceneReference;

        private AssetProvider _assetProvider;
        
        private BattleUIItemDescriptionView _battleUIItemDescriptionView;
        
        [Inject]
        private void Init(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public UniTask Load()
        {
            _battleUIItemDescriptionView = Instantiate(_assetProvider.GetAssetByName<BattleUIItemDescriptionView>(RuntimeConstants.AssetsName.ItemDescriptionView),
               _battleItemDescriptionSceneReference.BattleItemDescriptionWindow.transform);
           
            return UniTask.CompletedTask;
        }

        public void SetDescriptionPanelVisibility(bool inventoryVisibilityStatus)
        {
            _battleItemDescriptionSceneReference.BattleItemDescriptionWindow.SetActive(inventoryVisibilityStatus && _battleItemDescriptionSceneReference.BattleItemDescriptionWindow.activeSelf);
        }

        public void SetItemDescription(ItemBase itemBase)
        {
            _battleItemDescriptionSceneReference.BattleItemDescriptionWindow.SetActive(!_battleItemDescriptionSceneReference.BattleItemDescriptionWindow.activeSelf);
            
            if (_battleItemDescriptionSceneReference.BattleItemDescriptionWindow.activeSelf)
            {
                _battleUIItemDescriptionView.SetData(itemBase.ObjectInformation);
            }
        }
    }
}