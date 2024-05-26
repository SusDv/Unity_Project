using System;
using BattleModule.Utility;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Modifiers.Effects.Base;
using CharacterModule.Stats.Modifiers.Effects.Processor;
using CharacterModule.Utility;
using UnityEngine;
using Utility;

namespace CharacterModule.Stats.Modifiers.Base
{
    [Serializable]
    public abstract class TemporaryModifier<T> : ITemporaryModifier<T>
    {
        protected TemporaryEffect TemporaryEffect;
        
        protected TemporaryModifier(
            T type,
            ModifierData modifierData,
            TemporaryEffectType temporaryEffectType,
            int duration)
        {
            Type = type;
            
            ModifierData = modifierData;
            
            TemporaryEffectType = temporaryEffectType;

            Duration = duration;
        }
        
        [field: SerializeField]
        public T Type { get; private set; }

        [field: SerializeField]
        public TemporaryEffectType TemporaryEffectType { get; private set; }
        
        [field: SerializeField]
        public int Duration { get; set; }
        
        [field: SerializeField]
        public ModifierData ModifierData { get; private set; }
        
        public bool IsNegative => ModifierData.Value < 0;
        
        public BattleTimer BattleTimer { get; set; }
        

        public virtual void OnAdded()
        {
            TemporaryEffect = TemporaryEffectProcessor.GetEffect(TemporaryEffectType);
            
            TemporaryEffect.Init(ModifierData, BattleTimer, 
                    new Ref<int>(() => Duration, 
                        d => Duration = d));
        }

        public virtual void OnRemove()
        {
            TemporaryEffect.Remove();
        }

        public abstract IModifier<T> Clone();
        
        protected virtual bool Equals(TemporaryModifier<T> other)
        {
            return TemporaryEffectType == other.TemporaryEffectType &&
                   ModifierData.SourceID == other.ModifierData.SourceID &&
                   Mathf.RoundToInt(ModifierData.Value - other.ModifierData.Value) == 0;
        }
        
        public bool Equals(IModifier<T> modifier)
        {
            return Equals((TemporaryModifier<T>) modifier);
        }
    }
}