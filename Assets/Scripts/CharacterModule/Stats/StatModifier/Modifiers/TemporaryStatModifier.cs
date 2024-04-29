using System;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Manager;
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
        
        public TemporaryEffect TemporaryEffect { private get; set; }

        public int LocalCycle { get; private set; }

        public void OnAdded()
        {
            TemporaryEffect = TemporaryEffectProcessor.GetEffect(TemporaryEffectType).Init(this);
            
            LocalCycle = StatModifierManager.LocalCycle;
        }

        public void OnRemove()
        {
            TemporaryEffect.Remove();
        }

        public void Modify()
        {
            TemporaryEffect.Modify();
        }

        public IModifier Clone()
        {
            return new TemporaryStatModifier(StatType, ModifierData, TemporaryEffectType, Duration);
        }
    }
}
