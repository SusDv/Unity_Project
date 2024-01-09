using System;
using System.Collections.Generic;
using StatModule.Settings;
using StatModule.Utility.Enums;
using StatModule.Modifier;

namespace StatModule.Core
{
    [Serializable]
    public class Stats
    {
        private Dictionary<StatType, Stat> _stats;

        private List<BaseStatModifier> _modifiersInUse;

        public Action<Stats> OnStatsModified;

        public Stats(BaseStats baseStats)
        {
            _stats = new Dictionary<StatType, Stat>();

            Init(baseStats);
        }
        
        private void Init(BaseStats baseStats) 
        {
            _modifiersInUse = new List<BaseStatModifier>();

            foreach (var stat in baseStats.GetStats())
            {
                _stats.Add(stat.Key, stat.Value);
            }
        }

        private void AddStatModifier(BaseStatModifier statModifier) 
        {
            _modifiersInUse.Add(statModifier);
            ApplyStatModifier(statModifier);
        }

        private void RemoveStatModifier(BaseStatModifier statModifier) 
        {
            _modifiersInUse.Remove(statModifier);
        }

        private void ApplyStatModifier(BaseStatModifier statModifier) 
        {
            statModifier.Modify(_stats[statModifier.StatType], RemoveStatModifier);

            OnStatsModified?.Invoke(this);
        }

        public void ModifyStat(BaseStatModifier statModifier) 
        {
            AddStatModifier(statModifier.Clone() as BaseStatModifier);
        }

        public void ModifyStat(StatType statType, float value) 
        {
            AddStatModifier(InstantStatModifier.GetInstantStatModifierInstance(
                statType, ValueModifierType.ADDITIVE, value));
        }

        public float GetStatFinalValue(StatType type) 
        {
            return _stats[type].FinalValue;
        }

        public float GetStatBaseValue(StatType type) 
        {
            return _stats[type].BaseValue;
        }

        public float GetStatFinalValuesRatio(StatType numeratorStat, StatType denominatorStat) 
        {
            return GetStatFinalValue(numeratorStat) / GetStatFinalValue(denominatorStat);
        }
    
    }
}