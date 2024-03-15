using BattleModule.UI.BattleButton;
using CharacterModule.Inventory.Items.Base;
using InventorySystem.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.View 
{
    public class BattleUIItemView : BattleUIButton<BattleUIItemView>
    {
        [SerializeField] private Image _battleItemImage;
        [SerializeField] private TextMeshProUGUI _battleItemCount;

        public void SetData(InventoryItem inventoryItem)
        {
            _battleItemImage.sprite = inventoryItem.inventoryItem.ItemImage;

            _battleItemCount.text = inventoryItem.amount.ToString();
        }
        public BattleUIItemView CreateInstance(Transform parent)
        {
            return Instantiate(this, parent.transform.position, gameObject.transform.rotation, parent);
        }
    }
}