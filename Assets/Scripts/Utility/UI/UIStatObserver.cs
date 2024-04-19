using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;
using UnityEngine;

namespace Utility.UI
{
    public abstract class UIStatObserver : MonoBehaviour, IStatObserver
    {
        [field: SerializeField] 
        public StatType StatType { get; set; }

        public abstract void UpdateValue(StatInfo statInfo);
    }
}