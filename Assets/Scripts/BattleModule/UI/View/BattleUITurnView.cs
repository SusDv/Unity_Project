using System.Globalization;
using CharacterModule.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.View 
{
    public class BattleUITurnView : MonoBehaviour
    {
        [SerializeField] private Image _characterInTurnImage;

        [SerializeField] private TextMeshProUGUI _characterBattlePoints;

        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private Vector2 _widthDifference = new (15f, 5f);

        public void SetData(BaseInformation characterInfo,
            float battlePoints)
        {
            _characterInTurnImage.sprite = characterInfo.CharacterImage;

            _characterBattlePoints.text = battlePoints.ToString(CultureInfo.InvariantCulture);
        }

        public void SetPosition(BattleUITurnView prevView, int position)
        {
            _rectTransform.anchoredPosition = prevView._rectTransform.anchoredPosition;
            
            _rectTransform.sizeDelta -= _widthDifference * position;

            _rectTransform.anchoredPosition += Vector2.up * (-prevView._rectTransform.sizeDelta.y - 20f);
        }
    }
}

