using System;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifiers.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility.Enums;
using Utility;

namespace CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifiers
{
    public class TurnModifier : TemporaryModifier
    {
        public override TemporaryStatModifierType TemporaryStatModifierType =>
            TemporaryStatModifierType.APPLIED_EVERY_TURN;

        public override void Modify()
        {
            base.Modify();
            
            ValueModifierProcessor.ModifyStatValue(ValueToModify, TemporaryStatModifier);
        }
    }
}