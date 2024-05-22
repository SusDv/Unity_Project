using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Settings;
using CharacterModule.Stats.StatModifier.Modifiers;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.Managers
{
    public class StatManager : IStatSubject
    {
        private readonly List<IModifier> _modifiersInUse = new ();
        
        private readonly List<IStatObserver> _statObservers = new ();
        
        private readonly Dictionary<StatType, Stat> _stats = new ();
        
        private void NotifyObservers(StatType statType)
        {
            var statInfo = GetStatInfo(statType);
            
            foreach (var statObserver in _statObservers.Where(o => o.StatType == statType))
            {
                statObserver.UpdateValue(statInfo);
            }
        }
        
        private bool FoundExistingTemporaryModifier(ITemporaryModifier temporaryModifier)
        {
            if (!TryGetExistingModifier(temporaryModifier, out var existingModifier))
            {
                return false;
            }

            TriggerExistingModifier(existingModifier, temporaryModifier);
            
            return true;
        }

        private bool TryGetExistingModifier(IModifier temporaryModifier, out ITemporaryModifier existingModifier)
        {
            existingModifier = _modifiersInUse.OfType<ITemporaryModifier>().FirstOrDefault(t => t.Equals(temporaryModifier));

            return existingModifier != default;
        }
        
        private void TriggerExistingModifier(ITemporaryModifier existingModifier, ITemporaryModifier temporaryModifier)
        {
            existingModifier.BattleTimer.EndTimer();
            
            existingModifier.Duration = temporaryModifier.Duration;
        }

        private void InitializeModifier(IModifier modifier)
        {
            modifier.SetValueToModify(_stats[((IStatModifier)modifier).StatType]);

            modifier.OnAdded();
            
            NotifyObservers(((IStatModifier) modifier).StatType);
        }
        
        public StatManager(BaseStats baseStats)
        {
            foreach (var stat in baseStats.GetStats())
            {
                _stats.Add(stat.Key, stat.Value);
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
        
        public void AddModifier(IModifier statModifier)
        {
            InitializeModifier(statModifier);
            
            if (statModifier is InstantStatModifier)
            {
                return;
            }
            
            _modifiersInUse.Add(statModifier);
        }

        public void AddModifier(ITemporaryModifier temporaryModifier)
        {
            if (FoundExistingTemporaryModifier(temporaryModifier))
            {
                return;
            }
            
            temporaryModifier.BattleTimer = BattleEventManager.CreateTimer();
            
            InitializeModifier(temporaryModifier);
            
            _modifiersInUse.Add(temporaryModifier);
        }

        public void ApplyInstantModifier(StatType statType, float value)
        {
            AddModifier(InstantStatModifier.GetInstance(statType, value));
        }

        public void TriggerSealEffects()
        {
            foreach (var temporaryModifier in _modifiersInUse.OfType<ITemporaryModifier>().Where(modifier => modifier.TemporaryEffectType is TemporaryEffectType.SEAL_EFFECT))
            {
                temporaryModifier.BattleTimer.EndTimer();
                
                NotifyObservers(((IStatModifier) temporaryModifier).StatType);
            }
            
            RemoveModifiersOnCondition(modifier => modifier is ITemporaryModifier { Duration: <= 0 });
        }

        public void RemoveModifiersOnCondition(Func<IModifier, bool> conditionFunction)
        {
            _modifiersInUse.RemoveAll(RemoveConditionPredicate);
            
            return;

            bool RemoveConditionPredicate(IModifier modifier)
            {
                bool shouldRemove = conditionFunction.Invoke(modifier);

                if (shouldRemove)
                {
                    modifier.OnRemove();
                }
                
                NotifyObservers(((IStatModifier) modifier).StatType);

                return shouldRemove;
            }
        }
    }
}