using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Information;

namespace BattleModule.UI.View
{
    public class BattleUIItemDescriptionView : MonoBehaviour
    {
        [SerializeField] private Image _battleUIImage;
        
        [SerializeField] private TextMeshProUGUI _battleUIName;
        
        [SerializeField] private TextMeshProUGUI _battleUIDescription;
        
        public void SetData(ObjectInformation objectInformation)
        {
            _battleUIImage.sprite = objectInformation.Icon;
            
            _battleUIName.text = objectInformation.Name;
            
            _battleUIDescription.text = objectInformation.Description;
        }
    }
}