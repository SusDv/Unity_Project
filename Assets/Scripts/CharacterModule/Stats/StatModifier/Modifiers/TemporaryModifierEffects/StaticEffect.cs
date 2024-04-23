using CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifierEffects.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility.Enums;
using JetBrains.Annotations;
using Utility;

namespace CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifierEffects
{
    [UsedImplicitly]
    public class StaticEffect : TemporaryModifierEffect
    {
        public override TemporaryEffectType TemporaryEffectType =>
            TemporaryEffectType.STATIC_EFFECT;

        public override TemporaryModifierEffect Init(TemporaryStatModifier modifier, Ref<float> valueToModify)
        {
            ValueModifierProcessor.ModifyStatValue(valueToModify, modifier);
            
            return base.Init(modifier, valueToModify);
        }

        protected override void RemoveModifier()
        {
            base.RemoveModifier();
            
            ValueModifierProcessor.ModifyStatValue(ValueToModify, -TemporaryStatModifier);
        }
    }
}