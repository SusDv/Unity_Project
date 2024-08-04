using System.Collections.Generic;
using BattleModule.Actions.Types;
using CharacterModule.Inventory.Items.Base;
using BattleModule.Controllers.Modules;
using BattleModule.UI.Presenter.SceneReferences.Inventory;
using BattleModule.UI.View;
using BattleModule.Utility;
using BattleModule.Utility.Interfaces;
using Cysharp.Threading.Tasks;
using Utility;
using Utility.Constants;
using VContainer;
using UnityEngine;

namespace BattleModule.UI.Presenter 
{
    public class BattleUIInventory : MonoBehaviour, ILoadingUnit, 
        IUIElement, IBattleCancelable
    {
        [SerializeField]
        private BattleInventorySceneReference _battleInventorySceneReference;

        private AssetProvider _assetProvider;
        
        private BattleActionController _battleActionController;

        private BattleUIController _battleUIController;

        private BattleUIItemDescription _battleUIItemDescription;
        
        private BattleTransitionData _battleTransitionData;

        private BattleCancelableController _battleCancelableController;
        
        
        private List<BattleUIItemView> _battleUIItems = new ();
        
        private BattleUIItemView _battleUIItemView;

        
        [Inject]
        private void Init(AssetProvider assetProvider,
            BattleUIItemDescription battleUIItemDescription,
            BattleActionController battleActionController,
            BattleTransitionData battleTransitionData,
            BattleUIController battleUIController,
            BattleCancelableController battleCancelableController)
        {
            _assetProvider = assetProvider;
            
            _battleUIItemDescription = battleUIItemDescription;

            _battleActionController = battleActionController;
            
            _battleTransitionData = battleTransitionData;

            _battleUIController = battleUIController;

            _battleCancelableController = battleCancelableController;
        }

        public UniTask Load()
        {
            _battleUIItemView = _assetProvider.GetAssetByName<BattleUIItemView>(RuntimeConstants.AssetsName.ItemView);

            _battleInventorySceneReference.BattleInventoryButton.OnButtonClick += BattleInventoryButtonClicked;
            
            _battleTransitionData.PlayerInventory.OnInventoryChanged += UpdateBattleInventory;
            
            _battleUIController.AddAsUIElement(this);
            
            UpdateBattleInventory();

            return UniTask.CompletedTask;
        }

        public void ToggleVisibility()
        {
            _battleInventorySceneReference.BattleInventoryButton.gameObject.SetActive(!_battleInventorySceneReference.BattleInventoryButton.gameObject.activeSelf);
        }
        
        private void UpdateBattleInventory()
        { 
            BattleUIInventoryClear();

            foreach (var inventoryItem in _battleTransitionData.PlayerInventory.GetBattleInventory())
            {
                var battleUIItem =
                    _battleUIItemView.CreateInstance(_battleInventorySceneReference
                        .BattleInventoryItemsParent.transform);
                
                battleUIItem.OnButtonOver += BattleItemPointerOver;

                battleUIItem.OnButtonClick += BattleItemPointerClick;

                battleUIItem.SetData(inventoryItem);

                _battleUIItems.Add(battleUIItem);
            }
        }

        private void BattleUIInventoryClear()
        {
            _battleUIItems = new List<BattleUIItemView>();

            for (var i = 0; i < _battleInventorySceneReference.BattleInventoryItemsParent.transform.childCount; i++)
            {
                Destroy(_battleInventorySceneReference.BattleInventoryItemsParent.transform.GetChild(i).gameObject);
            }
        }

        private void SetInventoryVisibility(bool items)
        {
            _battleInventorySceneReference.BattleInventoryWindow.SetActive(items);
            
            _battleUIItemDescription.SetDescriptionPanelVisibility(!items);
        }

        private void BattleItemPointerOver(BattleUIItemView battleUIItem)
        {
            _battleUIItemDescription.SetItemDescription(GetSelectedItem(battleUIItem));
        }

        private void BattleItemPointerClick(BattleUIItemView battleUIItem) 
        {
            SetupBattleAction(battleUIItem);
            
            BattleInventoryButtonClicked();
        }

        private void SetupBattleAction(BattleUIItemView battleUIItem) 
        {
            _battleActionController.SetBattleAction<ItemAction>(GetSelectedItem(battleUIItem));
        }

        private void BattleInventoryButtonClicked()
        {
            SetInventoryVisibility(!_battleInventorySceneReference.BattleInventoryWindow.activeSelf);

            _battleCancelableController.TryAppendCancelable(this);
        }

        private ItemBase GetSelectedItem(BattleUIItemView battleUIItem) 
        {
            return _battleTransitionData.PlayerInventory.GetItem(_battleUIItems.IndexOf(battleUIItem)).Item;
        }

        public bool TryCancel()
        {
            if (!_battleInventorySceneReference.BattleInventoryWindow.activeSelf)
            {
                return false;
            }

            SetInventoryVisibility(false);
            
            return true;
        }
    }
}