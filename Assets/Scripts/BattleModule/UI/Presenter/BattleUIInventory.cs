using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BattleModule.Actions.BattleActions;
using BattleModule.Controllers;
using BattleModule.UI.Presenter.Settings.Inventory;
using BattleModule.UI.View;
using CharacterModule.Inventory.Items;
using InventorySystem.Intefaces;
using InventorySystem.Item;
using InventorySystem.Core;

namespace BattleModule.UI.Presenter 
{
    public class BattleUIInventory : MonoBehaviour
    {
        [SerializeField] private BattleInventorySettings _battleInventorySettings;
        
        [Space(10f)]
        
        [SerializeField] private BattleItemDescriptionSettings _battleItemDescriptionSettings;

        private List<BattleUIItemView> _battleUIItems = new ();
        
        private List<InventoryItem> _battleInventory = new ();

        private BattleActionController _battleActionController;

        public void Init(IBattleInvetory battleInventory, 
            BattleActionController battleActionController)
        {
            battleInventory.OnInventoryChanged += OnBattleInventoryChanged;

            _battleActionController = battleActionController;
            
            _battleInventorySettings.BattleInventoryButton.OnButtonClick += BattleInventoryItemClicked;
            
            _battleInventory = battleInventory.GetBattleInventory();
            
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
                var battleUIItem = Instantiate(_battleInventorySettings.BattleUIItemView,
                    _battleInventorySettings.BattleInventoryItemsParent.transform.position, Quaternion.identity,
                    _battleInventorySettings.BattleInventoryItemsParent.transform);

                battleUIItem.OnButtonOver += BattleItemPointerOver;

                battleUIItem.OnButtonClick += BattleItemPointerClick;

                battleUIItem.SetData(item.inventoryItem.ItemImage, item.amount.ToString());

                _battleUIItems.Add(battleUIItem);
            }
        }

        private void BattleUIInventoryClear()
        {
            _battleUIItems = new List<BattleUIItemView>();

            for (var i = 0; i < _battleInventorySettings.BattleInventoryItemsParent.transform.childCount; i++)
            {
                Destroy(_battleInventorySettings.BattleInventoryItemsParent.transform.GetChild(i).gameObject);
            }
        }

        private void BattleItemPointerOver(BattleUIItemView battleUIItem)
        {
            _battleItemDescriptionSettings.BattleItemDescriptionWindow.SetActive(!_battleItemDescriptionSettings.BattleItemDescriptionWindow.activeSelf);

            if (_battleItemDescriptionSettings.BattleItemDescriptionWindow.activeSelf)
            {
                _battleItemDescriptionSettings.BattleUIItemDescriptionView.SetData(GetSelectedItem(battleUIItem));
            }
        }

        private void BattleItemPointerClick(BattleUIItemView battleUIItem) 
        {
            SetupBattleAction(battleUIItem);
        }

        private void SetupBattleAction(BattleUIItemView battleUIItem) 
        {
            _battleActionController.SetBattleAction<BattleItemAction>(GetSelectedItem(battleUIItem));
        }

        private void BattleInventoryItemClicked(object o)
        {
            _battleInventorySettings.BattleInventoryWindow.SetActive(!_battleInventorySettings.BattleInventoryWindow.activeSelf);
            
            _battleItemDescriptionSettings.BattleItemDescriptionWindow.SetActive(_battleInventorySettings.BattleInventoryWindow.activeSelf && _battleItemDescriptionSettings.BattleItemDescriptionWindow.activeSelf);
        }

        private ItemBase GetSelectedItem(BattleUIItemView battleUIItem) 
        {
            return _battleInventory[_battleUIItems.IndexOf(battleUIItem)].inventoryItem;
        }
    }
}