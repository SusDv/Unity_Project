using UnityEngine;
using Utility.ObserverPattern;
using Utility.UI;

namespace BattleModule.UI.View
{
    public class BattleUIEnemyView : MonoBehaviour
    {
        [SerializeField] private SliderStatObserver _healthObserver;

        public void SetData(IStatSubject statSubject) 
        {
            statSubject.AttachStatObserver(_healthObserver);
        }
    }
}
