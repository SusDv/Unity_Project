using BattleModule.UI.Button;
using BattleModule.UI.View;
using BattleModule.Actions.BattleActions.Base;
using InventorySystem.Intefaces;
using InventorySystem.Item;
using InventorySystem.Core;
using System.Collections.Generic;
using BattleModule.Actions.BattleActions;
using UnityEngine;
using BattleModule.Controllers;

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

        [Header("BattleModule Button")]
        [SerializeField] private BattleUIDefaultButton _battleInventoryButton;

        private List<BattleUIItemView> _battleUIItems = new ();
        
        private List<InventoryItem> _battleInventory = new ();

        private BattleActionController _battleActionController;

        private IBattleInvetory _battleInventoryController;    

        private BattleUIItemView _selectedItem;

        public void Init(
            IBattleInvetory battleInventory,
            BattleActionController battleActionController)
        {
            _battleInventoryController = battleInventory;

            _battleActionController = battleActionController;

            battleInventory.OnInventoryChanged += BattleInventoryUpdate;

            _battleInventoryButton.OnButtonClick += BattleInventoryVisibility;

            BattleInventoryUpdate(_battleInventoryController.GetBattleInventory());          
        }

        private void BattleInventoryUpdate(List<InventoryItem> inventoryItems)
        {
            _battleInventory = inventoryItems;

            BattleUIInventoryClear();

            foreach (InventoryItem item in _battleInventory)
            {
                BattleUIItemView battleUIItem = Instantiate(_battleUIItemView,
                            _battleInventoryItemsParent.transform.position, Quaternion.identity,
                            _battleInventoryItemsParent.transform);

                battleUIItem.OnButtonOver += BattleItemPointerOver;

                battleUIItem.OnButtonClick += BattleItemPointerClick;

                battleUIItem.SetData(item.inventoryItem.ItemImage, item.amount.ToString());

                _battleUIItems.Add(battleUIItem);
            }
        }

        private void BattleUIInventoryClear()
        {
            _battleUIItems = new List<BattleUIItemView>();

            for (int i = 0; i < _battleInventoryItemsParent.transform.childCount; i++)
            {
                Destroy(_battleInventoryItemsParent.transform.GetChild(i).gameObject);
            }
        }

        private void BattleItemPointerOver(BattleUIItemView battleUIItem)
        {
            _battleItemDescriptionPanel.SetActive(!_battleItemDescriptionPanel.activeSelf);

            if (_battleItemDescriptionPanel.activeSelf)
            {
                _battleUIItemDescriptionView.SetData(
                    _battleInventory[_battleUIItems.IndexOf(battleUIItem)].inventoryItem);
            }
        }

        private void BattleItemPointerClick(BattleUIItemView battleUIItem) 
        {
            if (_selectedItem == null || _selectedItem != battleUIItem)
            {
                BattleInventoryVisibility(null);               
            }

            _selectedItem = _selectedItem == battleUIItem ? null : battleUIItem;

            SetupBattleAction();
        }

        private void SetupBattleAction() 
        {
            if (_selectedItem == null)
            {
                _battleActionController.ResetBattleAction();

                return;
            }

            _battleActionController.SetBattleAction<BattleItemAction>(GetSelectedItem());
        }

        private void BattleInventoryVisibility(object o)
        {
            _battleInventoryPanel.SetActive(!_battleInventoryPanel.activeSelf);

            _battleItemDescriptionPanel.SetActive(_battleInventoryPanel.activeSelf && _battleItemDescriptionPanel.activeSelf);
        }

        private ItemBase GetSelectedItem() 
        {
            return _battleInventoryController.GetBattleInventory()[_battleUIItems.IndexOf(_selectedItem)].inventoryItem;
        }
    }
}