using System;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Modifiers.Base;
using CharacterModule.Utility;
using CharacterModule.Utility.Stats;

namespace CharacterModule.Stats.Modifiers
{
    [Serializable]
    public class TemporaryStatModifier : TemporaryModifier<StatType>
    {
        private TemporaryStatModifier(
            StatType statType,
            ModifierData modifierData,
            TemporaryEffectType temporaryEffectType,
            int duration) : base(statType, modifierData, temporaryEffectType, duration)
        { }

        public override IModifier<StatType> Clone()
        {
            return new TemporaryStatModifier(Type, ModifierData.Clone(), TemporaryEffectType, Duration);
        }

        protected override bool Equals(TemporaryModifier<StatType> other)
        {
            return Type == other.Type && base.Equals(other);
        }
    }
}