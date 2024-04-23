using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifierEffects.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility.Enums;
using JetBrains.Annotations;
using Utility;

namespace CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifierEffects
{
    [UsedImplicitly]
    public class TimeEffect : TemporaryModifierEffect
    {
        public override TemporaryEffectType TemporaryEffectType =>
            TemporaryEffectType.TIME_EFFECT;
        
        private float _localCycleTimer;

        public override TemporaryModifierEffect Init(TemporaryStatModifier modifier, Ref<float> valueToModify)
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