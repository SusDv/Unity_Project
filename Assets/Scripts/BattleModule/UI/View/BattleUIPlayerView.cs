using System.Collections.Generic;
using System.Linq;
using CharacterModule.Stats.Interfaces;
using StatModule.Utility;
using StatModule.Utility.Enums;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace BattleModule.UI.View 
{
    public class BattleUIPlayerView : MonoBehaviour
    {
        [SerializeField] private Image _characterImage;
        
        [SerializeField] private List<SliderStatObserver> _statObservers;

        public List<SliderStatObserver> SetData(Sprite characterImage,
            StatInfo healthInfo, StatInfo manaInfo) 
        {
            _characterImage.sprite = characterImage;
            
            SetInitialData(StatType.HEALTH, healthInfo);
            
            SetInitialData(StatType.MANA, manaInfo);

            return _statObservers;
        }

        private void SetInitialData(StatType statType, StatInfo statInfo)
        {
            _statObservers.First(o => o.StatType == statType).UpdateValue(statInfo);
        }
    }
}