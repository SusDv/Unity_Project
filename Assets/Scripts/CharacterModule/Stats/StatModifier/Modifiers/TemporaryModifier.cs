using System;
using BattleModule.Utility;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Modifiers.Effects.Base;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public abstract class TemporaryModifier : ITemporaryModifier
    {
        protected TemporaryEffect TemporaryEffect;
        
        protected TemporaryModifier(
            ModifierData modifierData,
            TemporaryEffectType temporaryEffectType,
            int duration)
        {
            ModifierData = modifierData;
            
            TemporaryEffectType = temporaryEffectType;

            Duration = duration;
        }

        [field: SerializeField]
        public TemporaryEffectType TemporaryEffectType { get; private set; }
        
        [field: SerializeField]
        public int Duration { get; set; }
        
        [field: SerializeField]
        public ModifierData ModifierData { get; private set; }
        
        public BattleTimer BattleTimer { get; set; }

        public abstract void OnAdded();

        public abstract void OnRemove();

        public abstract IModifier Clone();
        
        public abstract bool Equals(IModifier modifier);
        
        protected virtual bool Equals(TemporaryModifier other)
        {
            return TemporaryEffectType == other.TemporaryEffectType &&
                   ModifierData.SourceID == other.ModifierData.SourceID &&
                   Mathf.RoundToInt(ModifierData.Value - other.ModifierData.Value) == 0;
        }
    }
}