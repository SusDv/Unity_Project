using BattleModule.Utility;
using CharacterModule.Stats.Modifiers.Effects.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Utility.Stats;
using Utility;

namespace CharacterModule.Stats.Modifiers.Effects
{
    public class StaticEffect : TemporaryEffect
    {
        public override void Init(ModifierData modifierData,
            BattleTimer battleTimer, Ref<int> duration)
        {
            base.Init(modifierData, battleTimer, duration);
            
            BattleTimer.StartTimer();
            
            ValueModifierProcessor.ModifyValue(ModifierData);
        }

        public override void Remove()
        {
            ValueModifierProcessor.ModifyValue(ModifierData.GetInverseModifier());

            base.Remove();
        }
    }
}