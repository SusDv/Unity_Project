using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Modifiers.Effects.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers.Effects
{
    public class StaticEffect : TemporaryEffect
    {
        public override TemporaryEffectType TemporaryEffectType =>
            TemporaryEffectType.STATIC_EFFECT;

        public override TemporaryEffect Init(ITemporaryModifier modifier)
        {
            ValueModifierProcessor.ModifyValue(TemporaryModifier.ModifierData.ValueToModify, modifier);
            
            return base.Init(modifier);
        }

        public override void Remove()
        {
            ValueModifierProcessor.ModifyValue(TemporaryModifier.ModifierData.ValueToModify, TemporaryModifier.GetInverseModifier());
        }
    }
}