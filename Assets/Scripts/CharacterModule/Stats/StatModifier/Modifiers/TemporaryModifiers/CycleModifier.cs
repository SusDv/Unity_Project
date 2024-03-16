using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifiers.Base;
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

        public override TemporaryModifier Init(TemporaryStatModifier modifier, Ref<float> valueToModify)
        {
            _localCycleTimer = BaseStatModifier.LocalCycle;
            
            return base.Init(modifier, valueToModify);
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