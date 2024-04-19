using System;
using System.Collections.Generic;
using System.Linq;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Settings;
using CharacterModule.Stats.StatModifier.Modifiers;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.Managers
{
    [Serializable]
    public class StatManager : IStatSubject
    {
        private Dictionary<StatType, Stat> _stats;

        private List<BaseStatModifier> _modifiersInUse;

        private List<IStatObserver> _statObservers;

        public StatManager(BaseStats baseStats)
        {
            Init(baseStats);
        }
        
        private void Init(BaseStats baseStats) 
        {
            _stats = new Dictionary<StatType, Stat>();

            _modifiersInUse = new List<BaseStatModifier>();

            _statObservers = new List<IStatObserver>();

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

        private void NotifyObservers(StatType statType)
        {
            var statInfo = GetStatInfo(statType);
            
            foreach (var statObserver in _statObservers.Where(o => o.StatType == statType))
            {
                statObserver.UpdateValue(statInfo);
            }
        }

        private List<BaseStatModifier> GetModifiersByCondition(Func<BaseStatModifier, bool> conditionFunction)
        {
            return _modifiersInUse.Where(conditionFunction).ToList();
        }

        public void ApplyStatModifier(BaseStatModifier statModifier) 
        {
            (statModifier.Clone() as BaseStatModifier)?.Init(_stats[statModifier.StatType], AddModifierToList, RemoveModifierFromList);
        }

        public void ApplyStatModifier(StatType statType, float value) 
        {
            BaseStatModifier statModifier = InstantStatModifier.GetInstantStatModifierInstance(
                statType, ValueModifierType.ADDITIVE, ModifiedValueType.FINAL_VALUE, value);

            statModifier.Init(_stats[statModifier.StatType], AddModifierToList, RemoveModifierFromList);
            
            NotifyObservers(statType);
        }

        public StatInfo GetStatInfo(StatType statType)
        {
            return StatInfo.GetInstance(_stats[statType]);
        }

        public void ApplyStatModifiersByCondition(Func<BaseStatModifier, bool> conditionFunction) 
        {
            foreach (var statModifier in GetModifiersByCondition(conditionFunction))
            {
                statModifier.Modify();
                    
                NotifyObservers(statModifier.StatType);
            }
        }

        public void RemoveStatModifiersByCondition(Func<BaseStatModifier, bool> conditionFunction)
        {
            foreach (var statModifier in GetModifiersByCondition(conditionFunction))
            {
                statModifier.Remove();
                    
                NotifyObservers(statModifier.StatType);
            }
        }

        public void AttachStatObserver(IStatObserver statObserver)
        {
            _statObservers.Add(statObserver);
            
            statObserver.UpdateValue(GetStatInfo(statObserver.StatType));
        }

        public void DetachStatObserver(IStatObserver statObserver)
        {
            _statObservers.Remove(statObserver);
        }
    }
}