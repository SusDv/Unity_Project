using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Modifiers;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Manager
{
    public class StatModifierManager
    {
        private readonly List<IModifier> _modifiersInUse;

        private readonly Dictionary<StatType, Stat> _statsRef;
        public event Action<StatType> OnModified = delegate { };

        private bool FoundExistingTemporaryModifier(IModifier modifier)
        {
            if (modifier is not ITemporaryModifier temporaryModifier 
                || !TryGetExistingModifier(temporaryModifier, out var existingModifier))
            {
                return false;
            }

            TriggerExistingModifier(existingModifier, temporaryModifier);
            
            return true;
        }

        private bool TryGetExistingModifier(IModifier modifier, out ITemporaryModifier existingModifier)
        {
            existingModifier = _modifiersInUse.OfType<ITemporaryModifier>().FirstOrDefault(t => t.Equals(modifier));

            return existingModifier != default;
        }
        
        private static void TriggerExistingModifier(ITemporaryModifier existingModifier, ITemporaryModifier temporaryModifier)
        {
            existingModifier.BattleTimer.EndTimer();
            
            existingModifier.Duration = temporaryModifier.Duration;
        }

        private void InitializeModifier(IModifier modifier, out IModifier preparedModifier)
        {
            preparedModifier = modifier.Clone();
            
            CheckTemporaryModifier(preparedModifier);
            
            preparedModifier.SetValueToModify(_statsRef[((IStatModifier)preparedModifier).StatType]);

            preparedModifier.OnAdded();
            
            OnModified?.Invoke(((IStatModifier) preparedModifier).StatType);
        }

        private void CheckTemporaryModifier(IModifier modifier)
        {
            if (modifier is not ITemporaryModifier temporaryModifier)
            {
                return;
            }
            
            temporaryModifier.BattleTimer = BattleEventManager.CreateTimer();
        }


        public void AddModifier(IModifier statModifier)
        {
            if (FoundExistingTemporaryModifier(statModifier))
            {
                return;
            }
            
            InitializeModifier(statModifier, out var preparedModifier);
            
            if (preparedModifier is InstantStatModifier)
            {
                return;
            }
            
            _modifiersInUse.Add(preparedModifier);
        }
        
        public StatModifierManager(Dictionary<StatType, Stat> stats)
        {
            _modifiersInUse = new List<IModifier>();
            
            _statsRef = stats;
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
                
                OnModified?.Invoke(((IStatModifier) temporaryModifier).StatType);
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
                
                OnModified?.Invoke(((IStatModifier) modifier).StatType);

                return shouldRemove;
            }
        }
    }
}