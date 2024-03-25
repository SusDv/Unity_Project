using CharacterModule.Inventory.Items.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.View
{
    public class BattleUIItemDescriptionView : MonoBehaviour
    {
        [SerializeField] private Image _battleUIImage;
        [SerializeField] private TextMeshProUGUI _battleUIName;
        [SerializeField] private TextMeshProUGUI _battleUIDescription;
        
        public void SetData(ItemBase battleItem)
        {
            _battleUIImage.sprite = battleItem.ItemImage;
            _battleUIName.text = battleItem.ItemName;
            _battleUIDescription.text = battleItem.ItemDescription;
        }
    }
}