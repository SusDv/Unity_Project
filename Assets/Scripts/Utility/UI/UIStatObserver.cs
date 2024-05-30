using CharacterModule.Utility;
using CharacterModule.Utility.Stats;
using UnityEngine;
using Utility.ObserverPattern;

namespace Utility.UI
{
    public abstract class UIStatObserver : MonoBehaviour, IStatObserver
    {
        [field: SerializeField] 
        public StatType StatType { get; set; }

        public abstract void UpdateValue(StatInfo statInfo);
    }
}