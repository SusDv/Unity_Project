using System;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Modifiers.Effects.Processor;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class TemporaryStatModifier : TemporaryModifier, IStatModifier
    {
        [field: SerializeField]
        public StatType StatType { get; private set; }
        
        protected TemporaryStatModifier(
            StatType statType,
            ModifierData modifierData,
            TemporaryEffectType temporaryEffectType,
            int duration) : base(modifierData, temporaryEffectType, duration)
        {
            StatType = statType;
        }

        public override void OnAdded()
        {
            TemporaryEffect = TemporaryEffectProcessor.GetEffect(TemporaryEffectType)
                .Init(this);
        }

        public override void OnRemove()
        {
            TemporaryEffect.Remove();
        }

        public override IModifier Clone()
        {
            return new TemporaryStatModifier(StatType, ModifierData.Clone(), TemporaryEffectType, Duration);
        }

        private bool Equals(TemporaryStatModifier other)
        {
            return StatType == other.StatType && base.Equals(other);
        }

        public override bool Equals(IModifier modifier)
        {
            return Equals((TemporaryStatModifier) modifier);
        }
    }
}
