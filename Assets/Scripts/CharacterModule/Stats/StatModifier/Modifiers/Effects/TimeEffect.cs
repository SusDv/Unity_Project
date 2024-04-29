using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Manager;
using CharacterModule.Stats.StatModifier.Modifiers.Effects.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers.Effects
{
    public class TimeEffect : TemporaryEffect
    {
        public override TemporaryEffectType TemporaryEffectType =>
            TemporaryEffectType.TIME_EFFECT;
        
        private float _localCycleTimer;

        public override TemporaryEffect Init(ITemporaryModifier modifier)
        {
            _localCycleTimer = StatModifierManager.LocalCycle;
            
            return base.Init(modifier);
        }

        protected override void DecreaseDuration()
        {
            if (--_localCycleTimer != 0)
            {
                return;
            }

            base.DecreaseDuration();
                
            ValueModifierProcessor.ModifyValue(TemporaryModifier.ModifierData.ValueToModify, TemporaryModifier);
            
            _localCycleTimer = StatModifierManager.LocalCycle;
        }
    }
}