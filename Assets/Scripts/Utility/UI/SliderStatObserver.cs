using System.Collections;
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

        private bool AnimationInProgress => Mathf.Abs(Mathf.RoundToInt(_animationSliderImage.fillAmount - _statSliderImage.fillAmount)) != 0;

        private void OnEnable()
        {
            _sliderText.gameObject.SetActive(_displayText);
        }

        public override void UpdateValue(StatInfo statInfo, bool negativeChange)
        {
            try
            {
                if (AnimationInProgress)
                {
                    StopCoroutine(AnimateSlider());
                }
            
                _statSliderImage.fillAmount = statInfo.FinalValue / statInfo.MaxValue;
            
                _sliderText.text = $"{Mathf.Round(statInfo.FinalValue)}";

                StartCoroutine(AnimateSlider());
            }
            catch
            {
                return;
            }
        }

        private IEnumerator AnimateSlider()
        {
            yield return new WaitForSeconds(0.2f);
            
            while (AnimationInProgress)
            {
                try
                {
                    _animationSliderImage.fillAmount =
                        Mathf.SmoothDamp(_animationSliderImage.fillAmount, _statSliderImage.fillAmount, ref _animationVelocity, 0.4f);
                }
                catch
                {
                    break;
                }
                
                yield return new WaitForSeconds(0.016f);
            }
            
            _animationSliderImage.fillAmount = _statSliderImage.fillAmount;
        }
        
        private void OnApplicationQuit()
        {
            StopCoroutine(AnimateSlider());
        }
    }
}