using System.Collections;
using CharacterModule.Utility;
using CharacterModule.Utility.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utility.UI
{
    public class SliderStatObserver : UIStatObserver
    {
        [SerializeField] private Image _statSliderImage;
        [SerializeField] private Image _animationSliderImage;

        [SerializeField] private TextMeshProUGUI _sliderText;

        [SerializeField] private bool _displayText = true;

        private float _animationVelocity;

        private void OnEnable()
        {
            _sliderText.gameObject.SetActive(_displayText);
        }

        public override void UpdateValue(StatInfo statInfo)
        {
            StopCoroutine(AnimateSlider());
            
            _statSliderImage.fillAmount = statInfo.FinalValue / statInfo.MaxValue;
            
            _sliderText.text = $"{statInfo.FinalValue}/{statInfo.MaxValue}";

            StartCoroutine(AnimateSlider());
        }

        private IEnumerator AnimateSlider()
        {
            yield return new WaitForSeconds(0.2f);
            
            while (Mathf.Abs(_animationSliderImage.fillAmount - _statSliderImage.fillAmount) > 0.001f)
            {
                _animationSliderImage.fillAmount =
                    Mathf.SmoothDamp(_animationSliderImage.fillAmount, _statSliderImage.fillAmount, ref _animationVelocity, 0.4f);
                
                yield return new WaitForSeconds(0.016f);
            }

            _animationSliderImage.fillAmount = _statSliderImage.fillAmount;

            _animationVelocity = 0;
        }
    }
}