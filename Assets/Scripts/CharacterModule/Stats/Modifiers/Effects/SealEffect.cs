using CharacterModule.Stats.Modifiers.Effects.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;

namespace CharacterModule.Stats.Modifiers.Effects
{
    public class SealEffect : TemporaryEffect
    {
        protected override void Modify()
        {
            base.Modify();
            
            ValueModifierProcessor.ModifyValue(ModifierData);
            
            BattleTimer.StopTimer();
        }
    }
}