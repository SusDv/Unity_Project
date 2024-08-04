using BattleModule.UI.BattleButton;
using CharacterModule.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.View 
{
    public class BattleUIItemView : BattleUIInteractable<BattleUIItemView>
    {
        [SerializeField] private Image _battleItemImage;
        
        [SerializeField] private TextMeshProUGUI _battleItemCount;

        public void SetData(InventoryItem inventoryItem)
        {
            _battleItemImage.sprite = inventoryItem.Item.ObjectInformation.Icon;

            _battleItemCount.text = inventoryItem.Amount.ToString();
        }
        public BattleUIItemView CreateInstance(Transform parent)
        {
            return Instantiate(this, 
                parent.transform.position, 
                gameObject.transform.rotation, 
                parent);
        }
    }
}