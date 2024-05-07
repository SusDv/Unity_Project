using System;
using BattleModule.Utility;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Modifiers.Effects.Base;
using CharacterModule.Stats.StatModifier.Modifiers.Effects.Processor;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class TemporaryStatModifier : ITemporaryModifier, IStatModifier
    {
        private TemporaryEffect _temporaryEffect;
        
        private TemporaryStatModifier(
            StatType statType,
            ModifierData modifierData,
            TemporaryEffectType temporaryEffectType,
            int duration)
        {
            StatType = statType;
            
            ModifierData = modifierData;
            
            TemporaryEffectType = temporaryEffectType;

            Duration = duration;
        }
        
        [field: SerializeField]
        public StatType StatType { get; private set; }

        [field: SerializeField]
        public TemporaryEffectType TemporaryEffectType { get; private set; }
        
        [field: SerializeField]
        public int Duration { get; set; }
        
        [field: SerializeField]
        public ModifierData ModifierData { get; private set; }
        
        public BattleTimer BattleTimer { get; set; }
        
        public void OnAdded()
        {
            _temporaryEffect = TemporaryEffectProcessor.GetEffect(TemporaryEffectType)
                .Init(this);
        }

        public void OnRemove()
        {
            _temporaryEffect.Remove();
        }

        public IModifier Clone()
        {
            return new TemporaryStatModifier(StatType, ModifierData.Clone(), TemporaryEffectType, Duration);
        }

        private bool Equals(TemporaryStatModifier other)
        {
            return StatType == other.StatType && TemporaryEffectType == other.TemporaryEffectType &&
                   ModifierData.SourceID == other.ModifierData.SourceID &&
                   Mathf.RoundToInt(ModifierData.Value - other.ModifierData.Value) == 0;
        }
        
        public bool Equals(IModifier modifier)
        {
            return Equals((TemporaryStatModifier) modifier);
        }
    }
}