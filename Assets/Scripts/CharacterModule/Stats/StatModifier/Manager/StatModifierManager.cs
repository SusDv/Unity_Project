using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public static int LocalCycle;

        public void AddModifier(IModifier statModifier)
        {
            var clone = statModifier.Clone();
            
            clone.SetValueToModify(_statsRef[((IStatModifier)clone).StatType]);
            
            clone.OnAdded();
            
            OnModified?.Invoke(((IStatModifier) clone).StatType);
            
            if (statModifier is InstantStatModifier)
            {
                return;
            }

            _modifiersInUse.Add(clone);
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

        public void TriggerSealAndStaticEffects()
        {
            foreach (var temporaryModifier in _modifiersInUse.OfType<ITemporaryModifier>().Where(modifier => modifier.TemporaryEffectType is not TemporaryEffectType.TIME_EFFECT))
            {
                temporaryModifier.Modify();
                
                OnModified?.Invoke(((IStatModifier) temporaryModifier).StatType);
            }
            
            RemoveModifiersOnCondition(modifier => modifier is ITemporaryModifier { Duration: <= 0 });
        }

        public void TriggerTimeEffects()
        {
            foreach (var temporaryModifier in _modifiersInUse.OfType<ITemporaryModifier>().Where(modifier => modifier.TemporaryEffectType is TemporaryEffectType.TIME_EFFECT))
            {
                temporaryModifier.Modify();
                
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

                modifier.OnRemove();
                
                OnModified?.Invoke(((IStatModifier) modifier).StatType);

                return shouldRemove;
            }
        }
    }
}