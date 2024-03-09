using System.Collections.Generic;
using UnityEngine;
using BattleModule.Actions.BattleActions;
using BattleModule.Controllers;
using BattleModule.UI.Presenter.SceneSettings.Inventory;
using BattleModule.UI.View;
using CharacterModule.Inventory;
using CharacterModule.Inventory.Items.Base;
using InventorySystem.Core;
using Utility;
using VContainer;

namespace BattleModule.UI.Presenter 
{
    public class BattleUIInventory : MonoBehaviour
    {
        private BattleInventorySceneSettings _battleInventorySceneSettings;
        
        private BattleActionController _battleActionController;

        private BattleUIItemDescription _battleUIItemDescription;
        
        
        private List<BattleUIItemView> _battleUIItems = new ();
        
        private List<InventoryItem> _battleInventory = new();


        [Inject]
        private void Init(BattleInventorySceneSettings battleInventorySceneSettings,
            BattleUIItemDescription battleUIItemDescription,
            BattleActionController battleActionController,
            BattleManager battleManager)
        {
            _battleInventorySceneSettings = battleInventorySceneSettings;
            
            _battleUIItemDescription = battleUIItemDescription;

            _battleActionController = battleActionController;
            
            _battleInventory = battleManager.PlayerInventory.GetBattleInventory();
            
            battleManager.PlayerInventory.OnInventoryChanged += OnBattleInventoryChanged;
            
            _battleInventorySceneSettings.BattleInventoryButton.OnButtonClick += BattleInventoryButtonClicked;
        }

        private void Start()
        {
            UpdateBattleInventory();
        }

        private void OnBattleInventoryChanged(InventoryBase playerInventory)
        {
            _battleInventory = playerInventory.GetBattleInventory();

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
            _battleUIItems = new List<BattleUIItemView>();

            for (var i = 0; i < _battleInventorySceneSettings.BattleInventoryItemsParent.transform.childCount; i++)
            {
                Destroy(_battleInventorySceneSettings.BattleInventoryItemsParent.transform.GetChild(i).gameObject);
            }
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