using System;
using System.Collections.Generic;
using StatModule.Settings;
using StatModule.Utility.Enums;
using StatModule.Modifier;
using System.Linq;
using StatModule.Interfaces;

namespace StatModule.Base
{
    [Serializable]
    public class Stats : IHaveStats
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

        private void AddModifierToList(BaseStatModifier statModifier) 
        {
            _modifiersInUse.Add(statModifier);
        }

        private void RemoveModifierFromList(BaseStatModifier statModifier) 
        {
            _modifiersInUse.Remove(statModifier);
        }

        private bool ExistingTemporaryStatModifierFound(BaseStatModifier statModifierAdded) 
        {
            TemporaryStatModifier existingTemporaryStatModifier = _modifiersInUse.FirstOrDefault(existingModifier => existingModifier.Equals(statModifierAdded)) as TemporaryStatModifier;

            if (existingTemporaryStatModifier != null)
            {
                existingTemporaryStatModifier.Duration = (statModifierAdded as TemporaryStatModifier)?.Duration ?? existingTemporaryStatModifier.Duration;

                return true;
            }

            return false;
        }

        public void AddStatModifier(BaseStatModifier statModifier) 
        {
            if (ExistingTemporaryStatModifierFound(statModifier))
            {
                return;
            }
            else 
            {
                statModifier.Modify(_stats[statModifier.StatType], AddModifierToList, RemoveModifierFromList);
            }

            OnStatsModified?.Invoke(this);
        }

        public void AddStatModifier(StatType statType, float value) 
        {
            BaseStatModifier statModifier = InstantStatModifier.GetInstantStatModifierInstance(
                statType, ValueModifierType.ADDITIVE, value);

            statModifier.Modify(_stats[statModifier.StatType], AddModifierToList, RemoveModifierFromList);
            
            OnStatsModified?.Invoke(this);
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

        public void ApplyStatModifiersByCondition(Func<BaseStatModifier, bool> conditionFunction, Func<BaseStatModifier, bool> additionalCondition = null) 
        {
            List<BaseStatModifier> modifiersByCondition = _modifiersInUse
                .Where(statModifier =>
                    conditionFunction.Invoke(statModifier))
                .ToList();

            if (additionalCondition != null) 
            {
                modifiersByCondition = modifiersByCondition
                    .Where(statModifier => 
                        additionalCondition.Invoke(statModifier))
                        .ToList();
            }

            modifiersByCondition.ForEach(statModifier => 
                {
                    statModifier.Modify(_stats[statModifier.StatType], AddModifierToList, RemoveModifierFromList);
                });

            OnStatsModified?.Invoke(this);
        }
    }
}