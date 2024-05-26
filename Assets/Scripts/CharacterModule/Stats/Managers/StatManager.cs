using System;
using System.Linq;
using System.Collections.Generic;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Modifiers;
using BattleModule.Utility;
using CharacterModule.Settings;
using CharacterModule.Utility;
using Utility.ObserverPattern;

namespace CharacterModule.Stats.Managers
{
    public class StatManager : IStatSubject
    {
        private readonly List<IModifier<StatType>> _modifiersInUse = new ();
        
        private readonly List<IStatObserver> _statObservers = new ();
        
        private readonly Dictionary<StatType, Stat> _stats = new ();

        private Func<int, BattleTimer> _battleTimerFactory;
        
        private void NotifyObservers(StatType statType)
        {
            var statInfo = GetStatInfo(statType);
            
            foreach (var statObserver in 
                     _statObservers
                         .Where(o => o.StatType == statType))
            {
                statObserver.UpdateValue(statInfo);
            }
        }
        
        private bool FindExistingTemporaryModifier(ITemporaryModifier<StatType> temporaryModifier)
        {
            if (!TryGetExistingModifier(temporaryModifier, 
                    out var existingModifier))
            {
                return false;
            }

            TriggerExistingModifier(existingModifier, temporaryModifier);
            
            return true;
        }

        private bool TryGetExistingModifier(IModifier<StatType> temporaryModifier, out ITemporaryModifier<StatType> existingModifier)
        {
            existingModifier = _modifiersInUse
                .OfType<ITemporaryModifier<StatType>>()
                .FirstOrDefault(t => 
                    t.Equals(temporaryModifier));

            return existingModifier != default;
        }
        
        private void TriggerExistingModifier(ITemporaryModifier<StatType> existingModifier, ITemporaryModifier<StatType> temporaryModifier)
        {
            existingModifier.Duration = temporaryModifier.Duration + 1;
            
            existingModifier.BattleTimer.EndTimer();
        }

        private void InitializeModifier(IModifier<StatType> modifier)
        {
            modifier.SetValueToModify(_stats[modifier.Type]);

            modifier.OnAdded();
            
            NotifyObservers(modifier.Type);
        }

        public StatManager(BaseStats baseStats)
        {
            foreach (var stat in baseStats.GetStats())
            {
                _stats.Add(stat.Key, stat.Value);
            }
        }

        public void SetBattleTimerFactory(Func<int, BattleTimer> battleTimerFactory)
        {
            _battleTimerFactory = battleTimerFactory;
        }

        public StatInfo GetStatInfo(StatType statType)
        {
            return StatInfo.GetInstance(_stats[statType]);
        }
        
        public void AddModifier(IModifier<StatType> statModifier)
        {
            InitializeModifier(statModifier);
            
            if (statModifier is InstantStatModifier)
            {
                return;
            }
            
            _modifiersInUse.Add(statModifier);
        }

        public void AddModifier(ITemporaryModifier<StatType> temporaryModifier)
        {
            if (FindExistingTemporaryModifier(temporaryModifier))
            {
                return;
            }
            
            temporaryModifier.BattleTimer = _battleTimerFactory.Invoke(0);

            InitializeModifier(temporaryModifier);
            
            _modifiersInUse.Add(temporaryModifier);
        }

        public void ApplyInstantModifier(StatType statType, float value)
        {
            AddModifier(InstantStatModifier.GetInstance(statType, value));
        }

        public void TriggerSealEffects()
        {
            foreach (var temporaryModifier in _modifiersInUse.OfType<ITemporaryModifier<StatType>>().Where(modifier => modifier.TemporaryEffectType is TemporaryEffectType.SEAL_EFFECT))
            {
                temporaryModifier.BattleTimer.EndTimer();
                
                NotifyObservers(temporaryModifier.Type);
            }
            
            RemoveModifiersOnCondition(m => m is TemporaryStatModifier {Duration: < 0});
        }

        public void RemoveModifiersOnCondition(Func<IModifier<StatType>, bool> conditionFunction)
        {
            _modifiersInUse.RemoveAll(RemoveConditionPredicate);
            
            return;

            bool RemoveConditionPredicate(IModifier<StatType> modifier)
            {
                if (!conditionFunction.Invoke(modifier))
                {
                    return false;
                }
                
                modifier.OnRemove();
                    
                NotifyObservers(modifier.Type);
                
                return true;
            }
        }

        public void ResetStatValue(StatType statType)
        {
            ApplyInstantModifier(statType, -GetStatInfo(statType).FinalValue);
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