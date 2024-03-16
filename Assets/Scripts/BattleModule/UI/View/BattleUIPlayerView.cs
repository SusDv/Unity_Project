using System.Collections.Generic;
using System.Linq;
using CharacterModule.Data.Info;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Utility.Enums;
using StatModule.Utility;
using UnityEngine;
using UnityEngine.UI;
using Utility.UI;

namespace BattleModule.UI.View 
{
    public class BattleUIPlayerView : MonoBehaviour
    {
        [SerializeField] private Image _characterImage;
        
        [SerializeField] private List<SliderStatObserver> _statObservers;

        public void SetData(CharacterInformation characterInformation, IStatSubject statSubject) 
        {
            _characterImage.sprite = characterInformation.CharacterImage;
            
            SetInitialData(StatType.HEALTH, statSubject.GetStatInfo(StatType.HEALTH));
            
            SetInitialData(StatType.MANA, statSubject.GetStatInfo(StatType.MANA));
            
            _statObservers.ForEach(statSubject.AttachStatObserver);
        }

        private void SetInitialData(StatType statType, StatInfo statInfo)
        {
            _statObservers.First(o => o.StatType == statType).UpdateValue(statInfo);
        }
    }
}