using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Modifiers.Effects.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers.Effects
{
    public class TimeEffect : TemporaryEffect
    {
        public override TemporaryEffectType TemporaryEffectType =>
            TemporaryEffectType.TIME_EFFECT;

        public override TemporaryEffect Init(ITemporaryModifier modifier)
        {
            base.Init(modifier);
            
            TemporaryModifier.BattleTimer.StartTimer();

            return this;
        }

        protected override void Modify()
        {
            base.Modify();
            
            ValueModifierProcessor.ModifyValue(TemporaryModifier.ModifierData.ValueToModify, TemporaryModifier);
        }
    }
}