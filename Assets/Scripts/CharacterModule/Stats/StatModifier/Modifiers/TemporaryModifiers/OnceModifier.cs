using System;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifiers.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility.Enums;
using Utility;

namespace CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifiers
{
    public class OnceModifier : TemporaryModifier
    {
        public override TemporaryStatModifierType TemporaryStatModifierType =>
            TemporaryStatModifierType.APPLIED_ONCE;

        public override TemporaryModifier Init(TemporaryStatModifier modifier, Ref<float> valueToModify)
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