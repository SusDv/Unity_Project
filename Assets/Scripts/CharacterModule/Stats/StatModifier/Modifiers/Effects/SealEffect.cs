using CharacterModule.Stats.StatModifier.Modifiers.Effects.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers.Effects
{
    public class SealEffect : TemporaryEffect
    {
        public override TemporaryEffectType TemporaryEffectType =>
            TemporaryEffectType.SEAL_EFFECT;

        protected override void Modify()
        {
            base.Modify();
            
            ValueModifierProcessor.ModifyValue(TemporaryModifier.ModifierData.ValueToModify, TemporaryModifier);
            
            TemporaryModifier.BattleTimer.StopTimer();
        }
    }
}