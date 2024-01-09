using UnityEngine;
using System.Collections.Generic;
using StatModule.Core;
using StatModule.Utility.Enums;

namespace StatModule.Settings
{
    [CreateAssetMenu(menuName = "Base Stats", fileName = "Character/Stats/Base Stats")]
    public class BaseStats : ScriptableObject 
    {
        [field: SerializeField]
        private List<Stat> _statList { get; set; } = new List<Stat>();

        private Dictionary<StatType, Stat> _stats = new Dictionary<StatType, Stat>();

        private void OnValidate()
        {
            _stats = new Dictionary<StatType, Stat>();

            foreach (Stat stat in _statList) 
            {
                stat.FinalValue = stat.BaseValue;
                _stats.TryAdd(stat.StatType, stat);
            }
        }

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

            foreach(Stat stat in _stats.Values) 
            {
                stats.Add(stat.StatType, stat.Clone() as Stat);
            }

            SetStatDependancy(stats, StatType.HEALTH, StatType.MAX_HEALTH);

            SetStatDependancy(stats, StatType.MANA, StatType.MAX_MANA);

            return stats;
        }
    }
}