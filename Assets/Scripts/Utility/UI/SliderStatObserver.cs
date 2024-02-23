using System;
using StatModule.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    [Serializable]
    public class SliderStatObserver : StatObserver
    {
        [SerializeField] private Image _sliderFillAmount;

        public override void UpdateValue(StatInfo statInfo)
        {
            _sliderFillAmount.fillAmount = statInfo.FinalValue / statInfo.MaxValue;
        }
    }
}