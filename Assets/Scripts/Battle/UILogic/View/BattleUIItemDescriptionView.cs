using InventorySystem.Item;
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

        private BaseItem _battleItem;

        public void SetData(BaseItem battleItem)
        {
            _battleItem = battleItem;
            GenerateDescription();
        }

        private void GenerateDescription()
        {
            _battleUIImage.sprite = _battleItem.ItemImage;
            _battleUIName.text = _battleItem.ItemName;
            _battleUIDescription.text = _battleItem.ItemDescription;
        }
    }
}