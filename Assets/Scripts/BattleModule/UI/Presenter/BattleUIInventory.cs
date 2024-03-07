using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BattleModule.Actions.BattleActions;
using BattleModule.Controllers;
using BattleModule.Transition;
using BattleModule.UI.Presenter.SceneSettings.Inventory;
using BattleModule.UI.View;
using CharacterModule.Inventory.Items;
using CharacterModule.Inventory.Items.Base;
using InventorySystem.Core;
using VContainer;

namespace BattleModule.UI.Presenter 
{
    public class BattleUIInventory : MonoBehaviour
    {
        private BattleInventorySceneSettings _battleInventorySceneSettings;
        
        private BattleActionController _battleActionController;

        private BattleUIItemDescription _battleUIItemDescription;
        
        
        private List<BattleUIItemView> _battleUIItems;
        
        private List<InventoryItem> _battleInventory;

        [Inject]
        private void Init(BattleUIItemDescription battleUIItemDescription, 
            BattleInventorySceneSettings battleInventorySceneSettings,
            BattleActionController battleActionController,
            BattleTransitionData battleTransitionManager)
        {
            _battleInventorySceneSettings = battleInventorySceneSettings;
            
            _battleUIItemDescription = battleUIItemDescription;
            
            _battleActionController = battleActionController;
            
            _battleInventory = battleTransitionManager.PlayerInventory.GetBattleInventory();
            
            _battleUIItems = new List<BattleUIItemView>();
            
            battleTransitionManager.PlayerInventory.OnInventoryChanged += OnBattleInventoryChanged;
            
            _battleInventorySceneSettings.BattleInventoryButton.OnButtonClick += BattleInventoryButtonClicked;

            UpdateBattleInventory();
        }

        private void OnBattleInventoryChanged(List<InventoryItem> inventoryItems)
        {
            _battleInventory = inventoryItems.Where(item => item.inventoryItem is ConsumableItem).ToList();

            UpdateBattleInventory();
        }

        private void UpdateBattleInventory()
        {
            BattleUIInventoryClear();

            foreach (var item in _battleInventory)
            {
                var battleUIItem = Instantiate(_battleInventorySceneSettings.BattleUIItemView,
                    _battleInventorySceneSettings.BattleInventoryItemsParent.transform.position, Quaternion.identity,
                    _battleInventorySceneSettings.BattleInventoryItemsParent.transform);

                battleUIItem.OnButtonOver += BattleItemPointerOver;

                battleUIItem.OnButtonClick += BattleItemPointerClick;

                battleUIItem.SetData(item.inventoryItem.ItemImage, item.amount.ToString());

                _battleUIItems.Add(battleUIItem);
            }
        }

        private void BattleUIInventoryClear()
        {
            _battleUIItems.ForEach(uiItem => uiItem.Dispose());
            
            _battleUIItems.Clear();
        }

        private void BattleItemPointerOver(BattleUIItemView battleUIItem)
        {
            _battleUIItemDescription.SetItemDescription(GetSelectedItem(battleUIItem));
        }

        private void BattleItemPointerClick(BattleUIItemView battleUIItem) 
        {
            SetupBattleAction(battleUIItem);
        }

        private void SetupBattleAction(BattleUIItemView battleUIItem) 
        {
            _battleActionController.SetBattleAction<BattleItemAction>(GetSelectedItem(battleUIItem));
        }

        private void BattleInventoryButtonClicked(object o)
        {
            _battleInventorySceneSettings.BattleInventoryWindow.SetActive(!_battleInventorySceneSettings.BattleInventoryWindow.activeSelf);
            
            _battleUIItemDescription.SetDescriptionPanelVisibility(_battleInventorySceneSettings.BattleInventoryWindow.activeSelf);
        }

        private ItemBase GetSelectedItem(BattleUIItemView battleUIItem) 
        {
            return _battleInventory[_battleUIItems.IndexOf(battleUIItem)].inventoryItem;
        }
    }
}