using System.Collections.Generic;
using InventorySystem.Core;
using BattleModule.UI.Button;
using BattleModule.UI.View;
using UnityEngine;
using BattleModule.ActionCore;
using BattleModule.ActionCore.Context;
using BattleModule.ActionCore.Interfaces;
using InventorySystem.Intefaces;
using BattleModule.ActionCore.Events;
using InventorySystem.Item;

namespace BattleModule.UI.Presenter 
{
    public class BattleUIInventory : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject _battleInventoryPanel;
        [SerializeField] private GameObject _battleInventoryItemsParent;
        [SerializeField] private GameObject _battleItemDescriptionPanel;

        [Header("Views")]
        [SerializeField] private BattleUIItemView _battleUIItemView;
        [SerializeField] private BattleUIItemDescriptionView _battleUIItemDescriptionView;

        [Header("Battle Button")]
        [SerializeField] private BattleUIButton _battleInventoryButton;

        private List<BattleUIItemView> _battleUIItems;

        private List<InventoryItem> _battleInventoryItems;

        private BattleUIItemView _selectedItem;

        private IBattleAction _battleActionController;

        public void InitBattleInventory(
            IBattleInvetory battleInventory,
            IBattleAction battleActionController)
        {
            _battleActionController = battleActionController;

            battleInventory.OnInventoryChanged += BattleInventoryUpdate;

            _battleInventoryButton.OnButtonClick += BattleInventoryVisibility;

            BattleInventoryUpdate(battleInventory.GetBattleInventory());
        }

        private void BattleInventoryClear()
        {
            for (int i = 0; i < _battleInventoryItemsParent.transform.childCount; i++)
            {
                Destroy(_battleInventoryItemsParent.transform.GetChild(i).gameObject);
            }
        }

        private void BattleInventoryUpdate(List<InventoryItem> inventoryItems)
        {
            _battleInventoryItems = inventoryItems;

            BattleInventoryClear();

            _battleUIItems = new List<BattleUIItemView>();

            foreach (InventoryItem item in _battleInventoryItems)
            {
                CreateBattleUIItemInstance(item);
            }
        }

        private void CreateBattleUIItemInstance(InventoryItem item)
        {
            BattleUIItemView battleUIItem = Instantiate(_battleUIItemView,
                            _battleInventoryItemsParent.transform.position, Quaternion.identity,
                            _battleInventoryItemsParent.transform);

            battleUIItem.OnItemOver += BattleItemPointerOver;
            battleUIItem.OnItemClick += BattleItemPointerClick;

            battleUIItem.SetData(item.inventoryItem.ItemImage, item.amount.ToString());

            _battleUIItems.Add(battleUIItem);
        }

        private void BattleItemPointerOver(BattleUIItemView battleUIItem)
        {
            _battleItemDescriptionPanel.SetActive(!_battleItemDescriptionPanel.activeSelf);

            if (_battleItemDescriptionPanel.activeSelf)
            {
                _battleUIItemDescriptionView.SetData(
                    _battleInventoryItems[_battleUIItems.IndexOf(battleUIItem)].inventoryItem);
            }
        }

        private void BattleItemPointerClick(BattleUIItemView battleUIItem) 
        {
            if (_selectedItem == null || _selectedItem != battleUIItem)
            {
                BattleInventoryVisibility();               
            }

            _selectedItem = _selectedItem == battleUIItem ? null : battleUIItem;

            SetupBattleItemAction();
        }

        private void SetupBattleItemAction() 
        {
            if (_selectedItem == null)
            {
                _battleActionController.ResetBattleAction();

                return;
            }

            _battleActionController.SetBattleAction<BattleItemAction>(GetSelectedItem());
        }

        public void BattleInventoryVisibility()
        {
            _battleInventoryPanel.SetActive(!_battleInventoryPanel.activeSelf);

            _battleItemDescriptionPanel.SetActive(
                _battleInventoryPanel.activeSelf ? _battleItemDescriptionPanel.activeSelf : false);
        }

        public BaseItem GetSelectedItem() 
        {
            return _battleInventoryItems[_battleUIItems.IndexOf(_selectedItem)].inventoryItem;
        }
    }
}