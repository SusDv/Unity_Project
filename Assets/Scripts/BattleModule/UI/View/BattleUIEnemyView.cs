using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Utility.Enums;
using UnityEngine;
using Utility.UI;

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
