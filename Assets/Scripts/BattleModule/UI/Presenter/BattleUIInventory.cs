using System.Collections.Generic;
using BattleModule.Actions.Types;
using CharacterModule.Inventory;
using CharacterModule.Inventory.Items.Base;
using BattleModule.Controllers.Modules;
using BattleModule.UI.Presenter.SceneReferences.Inventory;
using BattleModule.UI.View;
using BattleModule.Utility;
using Cysharp.Threading.Tasks;
using Utility;
using Utility.Constants;
using VContainer;
using UnityEngine;

namespace BattleModule.UI.Presenter 
{
    public class BattleUIInventory : MonoBehaviour, ILoadingUnit
    {
        [SerializeField]
        private BattleInventorySceneReference _battleInventorySceneReference;

        private AssetProvider _assetProvider;
        
        private BattleActionController _battleActionController;

        private BattleUIItemDescription _battleUIItemDescription;
        
        private BattleTransitionData _battleTransitionData;

        private InventoryBase _battleInventory;
        
        
        private List<BattleUIItemView> _battleUIItems = new ();
        
        private BattleUIItemView _battleUIItemView;

        [Inject]
        private void Init(AssetProvider assetProvider,
            BattleUIItemDescription battleUIItemDescription,
            BattleActionController battleActionController,
            BattleTransitionData battleTransitionData)
        {
            _assetProvider = assetProvider;
            
            _battleUIItemDescription = battleUIItemDescription;

            _battleActionController = battleActionController;
            
            _battleTransitionData = battleTransitionData;
        }

        public UniTask Load()
        {
            _battleUIItemView = _assetProvider.GetAssetByName<BattleUIItemView>(RuntimeConstants.AssetsName.ItemView);

            _battleInventorySceneReference.BattleInventoryButton.OnButtonClick += BattleInventoryButtonClicked;

            _battleInventory = _battleTransitionData.PlayerInventory;
            
            _battleTransitionData.PlayerInventory.OnInventoryChanged += OnInventoryChanged;

            UpdateBattleInventory();

            return UniTask.CompletedTask;
        }

        private void OnInventoryChanged(InventoryBase playerInventory)
        {
            UpdateBattleInventory();
        }

        private void UpdateBattleInventory()
        { 
            BattleUIInventoryClear();

            foreach (var inventoryItem in _battleInventory.GetBattleInventory())
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

        private void BattleInventoryButtonClicked()
        {
            _battleInventorySceneReference.BattleInventoryWindow.SetActive(!_battleInventorySceneReference.BattleInventoryWindow.activeSelf);
            
            _battleUIItemDescription.SetDescriptionPanelVisibility(_battleInventorySceneReference.BattleInventoryWindow.activeSelf);
        }

        private ItemBase GetSelectedItem(BattleUIItemView battleUIItem) 
        {
            return _battleInventory.GetItem(_battleUIItems.IndexOf(battleUIItem)).Item;
        }
    }
}