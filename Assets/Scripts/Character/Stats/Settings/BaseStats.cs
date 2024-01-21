using UnityEngine;
using System.Collections.Generic;
using StatModule.Base;
using StatModule.Utility.Enums;

namespace StatModule.Settings
{
    [CreateAssetMenu(menuName = "Base Stats", fileName = "Character/Stats/Base Stats")]
    public class BaseStats : ScriptableObject 
    {
        [field: SerializeField]
        private List<Stat> _statList { get; set; } = new List<Stat>();

#if UNITY_EDITOR
        private void OnValidate()
        {
            foreach (Stat stat in _statList) 
            {
                stat.FinalValue = stat.BaseValue;
            }
        }
#else
        private void Awake()
        {
            foreach (Stat stat in _statList)
            {
                stat.FinalValue = stat.BaseValue;
            }
        }
#endif

        private void SetStatDependancy(Dictionary<StatType, Stat> stats, StatType stat, StatType dependendOnStat) 
        {
            if (stats.TryGetValue(stat, out Stat finalStat) &&
                stats.TryGetValue(dependendOnStat, out Stat maxStat))
            {
                finalStat.DependencyWithStat = maxStat;

                maxStat.DependencyWithStat = finalStat;
            }
        }

        public Dictionary<StatType, Stat> GetStats() 
        {
            Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();

            foreach(Stat stat in _statList) 
            {
                stats.Add(stat.StatType, stat.Clone() as Stat);
            }

            SetStatDependancy(stats, StatType.HEALTH, StatType.MAX_HEALTH);

            SetStatDependancy(stats, StatType.MANA, StatType.MAX_MANA);

            return stats;
        }
    }
}