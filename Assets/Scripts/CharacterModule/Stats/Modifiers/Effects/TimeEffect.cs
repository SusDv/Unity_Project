using BattleModule.Utility;
using CharacterModule.Stats.Modifiers.Effects.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Utility;
using CharacterModule.Utility.Stats;
using Utility;

namespace CharacterModule.Stats.Modifiers.Effects
{
    public class TimeEffect : TemporaryEffect
    {
        public override void Init(ModifierData modifierData,
            BattleTimer battleTimer, Ref<int> duration)
        {
            base.Init(modifierData, battleTimer, duration);
            
            BattleTimer.StartTimer();
        }

        protected override void Modify()
        {
            base.Modify();
            
            ValueModifierProcessor.ModifyValue(ModifierData);
        }
    }
}