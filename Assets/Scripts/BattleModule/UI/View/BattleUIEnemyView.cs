using CharacterModule.Stats.Interfaces;
using StatModule.Utility.Enums;
using UnityEngine;
using Utils;

namespace BattleModule.UI.View
{
    public class BattleUIEnemyView : MonoBehaviour
    {
        [SerializeField] private SliderStatObserver _healthObserver;

        public void SetData(IStatSubject statSubject) 
        {
            _healthObserver.UpdateValue(statSubject.GetStatInfo(StatType.HEALTH));
            
            statSubject.AttachStatObserver(_healthObserver);
        }
    }
}
