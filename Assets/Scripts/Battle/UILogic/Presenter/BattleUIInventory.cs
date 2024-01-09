using System;
using System.Collections.Generic;
using InventorySystem.Core;
using BattleModule.UI.Button;
using BattleModule.UI.View;
using UnityEngine;
using BattleModule.ActionCore;
using BattleModule.ActionCore.Events;

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

        public void InitBattleInventory(
            ref Action<List<InventoryItem>> battleItemsChanged, 
            List<InventoryItem> battleInventory)
        {      
            battleItemsChanged += BattleInventoryUpdate;

            _battleInventoryButton.OnButtonClick += BattleInventoryVisibility;

            BattleInventoryUpdate(battleInventory);
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

            if (_selectedItem == null)
            {
                BattleGlobalActionEvent.BattleAction = BattleDefaultAction.GetBattleDefaultActionInstance();
                return;
            }

            SetupBattleItemAction();
        }

        private void SetupBattleItemAction() 
        {
            BattleItemAction battleItemAction = new BattleItemAction();

            battleItemAction.SetupAction(GetSelectedItem());

            BattleGlobalActionEvent.BattleAction = battleItemAction;
        }


        public void BattleInventoryVisibility()
        {
            _battleInventoryPanel.SetActive(!_battleInventoryPanel.activeSelf);

            _battleItemDescriptionPanel.SetActive(
                _battleInventoryPanel.activeSelf ? _battleItemDescriptionPanel.activeSelf : false);
        }

        public InventoryItem GetSelectedItem() 
        {
            return _battleInventoryItems[_battleUIItems.IndexOf(_selectedItem)];
        }
    }
}