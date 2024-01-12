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

        private void AddStatModifierCallback(BaseStatModifier statModifier) 
        {
            _modifiersInUse.Add(statModifier);
        }

        private void RemoveStatModifierCallback(BaseStatModifier statModifier) 
        {
            _modifiersInUse.Remove(statModifier);
        }

        public void ApplyStatModifier(BaseStatModifier statModifier) 
        {
            statModifier.Modify(_stats[statModifier.StatType], AddStatModifierCallback, RemoveStatModifierCallback);

            OnStatsModified?.Invoke(this);
        }

        public void ApplyStatModifier(StatType statType, float value) 
        {
            ApplyStatModifier(InstantStatModifier.GetInstantStatModifierInstance(
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

        public IList<BaseStatModifier> GetBaseStatModifiers() 
        {
            return _modifiersInUse.AsReadOnly();
        }
    
    }
}