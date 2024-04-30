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
        
        protected override void DecreaseDuration()
        {
            if (--TemporaryModifier.LocalCycle > 0)
            {
                return;
            }

            base.DecreaseDuration();
                
            ValueModifierProcessor.ModifyValue(TemporaryModifier.ModifierData.ValueToModify, TemporaryModifier);
            
            TemporaryModifier.LocalCycle = StatModifierManager.LocalCycle;
        }
    }
}