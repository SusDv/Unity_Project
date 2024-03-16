using System;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility.Enums;
using Utility;

namespace CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifiers
{
    public class CycleModifier : TemporaryModifier
    {
        public override TemporaryStatModifierType TemporaryStatModifierType =>
            TemporaryStatModifierType.APPLIED_EVERY_CYCLE;
        
        private float _localCycleTimer;

        public override TemporaryModifier Init(TemporaryStatModifier modifier, Ref<float> valueToModify, Action<BaseStatModifier> removeModifierCallback)
        {
            _localCycleTimer = modifier.LocalCycleTimer;
            
            return base.Init(modifier, valueToModify, removeModifierCallback);
        }

        protected override void DecreaseDuration()
        {
            if (--_localCycleTimer != 0)
            {
                return;
            }

            base.DecreaseDuration();
                
            ValueModifierProcessor.ModifyStatValue(ValueToModify, TemporaryStatModifier);
        }
    }
}