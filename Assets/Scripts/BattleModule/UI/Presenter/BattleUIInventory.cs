using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using BattleModule.Actions.BattleActions.ActionTypes;
using BattleModule.Controllers.Modules;
using BattleModule.UI.Presenter.SceneSettings.Inventory;
using BattleModule.UI.View;
using BattleModule.Utility;
using CharacterModule.Inventory;
using CharacterModule.Inventory.Items.Base;
using Utility;
using VContainer;

namespace BattleModule.UI.Presenter 
{
    public class BattleUIInventory : MonoBehaviour, ILoadingUnit
    {
        private BattleInventorySceneSettings _battleInventorySceneSettings;
        
        private BattleActionController _battleActionController;

        private BattleUIItemDescription _battleUIItemDescription;
        
        private BattleTransitionData _battleTransitionData;
        
        
        private List<BattleUIItemView> _battleUIItems = new ();
        
        private List<InventoryItem> _battleInventory = new();


        [Inject]
        private void Init(BattleInventorySceneSettings battleInventorySceneSettings,
            BattleUIItemDescription battleUIItemDescription,
            BattleActionController battleActionController,
            BattleTransitionData battleTransitionData)
        {
            _battleInventorySceneSettings = battleInventorySceneSettings;
            
            _battleUIItemDescription = battleUIItemDescription;

            _battleActionController = battleActionController;
            
            _battleTransitionData = battleTransitionData;
        }

        public Task Load()
        {
            _battleInventorySceneSettings.BattleInventoryButton.OnButtonClick += BattleInventoryButtonClicked;
        
            _battleTransitionData.PlayerInventory.OnInventoryChanged += OnInventoryChanged;
        
            _battleInventory = _battleTransitionData.PlayerInventory.GetBattleInventory();
            
            UpdateBattleInventory();

            return Task.CompletedTask;
        }

        private void OnInventoryChanged(InventoryBase playerInventory)
        {
            _battleInventory = playerInventory.GetBattleInventory();

            UpdateBattleInventory();
        }

        private void UpdateBattleInventory()
        { 
            BattleUIInventoryClear();

            foreach (var inventoryItem in _battleInventory)
            {
                var battleUIItem =
                    _battleInventorySceneSettings.BattleUIItemView.CreateInstance(_battleInventorySceneSettings
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
            _battleActionController.SetBattleAction<ItemAction>(GetSelectedItem(battleUIItem));
        }

        private void BattleInventoryButtonClicked(object o)
        {
            _battleInventorySceneSettings.BattleInventoryWindow.SetActive(!_battleInventorySceneSettings.BattleInventoryWindow.activeSelf);
            
            _battleUIItemDescription.SetDescriptionPanelVisibility(_battleInventorySceneSettings.BattleInventoryWindow.activeSelf);
        }

        private ItemBase GetSelectedItem(BattleUIItemView battleUIItem) 
        {
            return _battleInventory[_battleUIItems.IndexOf(battleUIItem)].Item;
        }
    }
}