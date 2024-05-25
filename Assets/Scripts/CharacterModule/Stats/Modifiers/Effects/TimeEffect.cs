using System;
using BattleModule.Utility;
using CharacterModule.Stats.Modifiers.Effects.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Utility;
using Utility;

namespace CharacterModule.Stats.Modifiers.Effects
{
    public class TimeEffect : TemporaryEffect
    {
        public override void Init(ModifierData modifierData,
            BattleTimer battleTimer, Ref<int> duration, Action removeModifier)
        {
            base.Init(modifierData, battleTimer, duration, removeModifier);
            
            BattleTimer.StartTimer();
        }

        protected override void Modify()
        {
            base.Modify();
            
            ValueModifierProcessor.ModifyValue(ModifierData);
        }
    }
}