using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Modifiers.Effects.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers.Effects
{
    public class StaticEffect : TemporaryEffect
    {
        public override TemporaryEffectType TemporaryEffectType => TemporaryEffectType.STATIC_EFFECT;

        public override TemporaryEffect Init(ITemporaryModifier modifier)
        {
            base.Init(modifier);
            
            TemporaryModifier.BattleTimer.StartTimer();
            
            ValueModifierProcessor.ModifyValue(TemporaryModifier.ModifierData.ValueToModify, TemporaryModifier);

            return this;
        }

        public override void Remove()
        {
            ValueModifierProcessor.ModifyValue(TemporaryModifier.ModifierData.ValueToModify, TemporaryModifier.GetInverseModifier());
        }
    }
}