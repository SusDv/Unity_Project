using System;
using CharacterModule.Stats.Interfaces;
using StatModule.Utility;
using StatModule.Utility.Enums;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public abstract class StatObserver : IStatObserver
    {
        [field: SerializeField] 
        public StatType StatType { get; set; }

        public abstract void UpdateValue(StatInfo statInfo);
    }
}