using System;
using System.Collections.Generic;
using System.Linq;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Settings;
using CharacterModule.Stats.StatModifier.Manager;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.Managers
{
    [Serializable]
    public class StatManager : IStatSubject
    {
        private Dictionary<StatType, Stat> _stats = new ();

        private List<IStatObserver> _statObservers = new ();

        public StatModifierManager StatModifierManager;

        public StatManager(BaseStats baseStats)
        {
            Init(baseStats);
        }
        
        private void Init(BaseStats baseStats) 
        {
            foreach (var stat in baseStats.GetStats())
            {
                _stats.Add(stat.Key, stat.Value);
            }
            
            StatModifierManager = new StatModifierManager(_stats);

            StatModifierManager.OnModified += NotifyObservers;
        }

        private void NotifyObservers(StatType statType)
        {
            var statInfo = GetStatInfo(statType);
            
            foreach (var statObserver in _statObservers.Where(o => o.StatType == statType))
            {
                statObserver.UpdateValue(statInfo);
            }
        }

        public StatInfo GetStatInfo(StatType statType)
        {
            return StatInfo.GetInstance(_stats[statType]);
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