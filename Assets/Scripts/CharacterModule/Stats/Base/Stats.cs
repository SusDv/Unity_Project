using System;
using System.Collections.Generic;
using System.Linq;
using CharacterModule.Stats.StatModifier.Modifiers;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using StatModule.Settings;
using StatModule.Utility.Enums;

namespace CharacterModule.Stats.Base
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
            if (_modifiersInUse.FirstOrDefault(existingModifier => existingModifier.Equals(statModifierAdded)) is not TemporaryStatModifier existingTemporaryStatModifier)
            {
                return false;
            }
            
            existingTemporaryStatModifier.Duration = (statModifierAdded as TemporaryStatModifier)?.Duration ?? existingTemporaryStatModifier.Duration;

            return true;
        }

        public void AddStatModifier(BaseStatModifier statModifier) 
        {
            if (ExistingTemporaryStatModifierFound(statModifier))
            {
                return;
            }
            
            statModifier.Modify(_stats[statModifier.StatType], AddModifierToList, RemoveModifierFromList);

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

        public void ApplyStatModifiersByCondition(Func<BaseStatModifier, bool> conditionFunction) 
        {
            _modifiersInUse
                .Where(conditionFunction)
                .ToList()
                .ForEach(statModifier => 
                {
                    statModifier.Modify(_stats[statModifier.StatType], AddModifierToList, RemoveModifierFromList);
                });

            OnStatsModified?.Invoke(this);
        }

        public void RemoveStatModifiersByCondition(Predicate<BaseStatModifier> conditionFunction) 
        {
            _modifiersInUse.RemoveAll(conditionFunction);
        }
    }
}