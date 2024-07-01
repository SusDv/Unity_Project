using System;
using System.Collections.Generic;
using CharacterModule.Stats.Modifiers.Effects.Base;
using CharacterModule.Utility;

namespace CharacterModule.Stats.Modifiers.Effects.Processor
{
    public static class TemporaryEffectProcessor
    {
        private static readonly Dictionary<StatusEffectType, Func<TemporaryEffect>> TemporaryModifiers = new()
        {
            { StatusEffectType.STATIC_EFFECT , () => new StaticEffect()},
            { StatusEffectType.SEAL_EFFECT , () => new SealEffect()},
            { StatusEffectType.TIME_EFFECT , () => new TimeEffect()}
        };
        
        public static TemporaryEffect GetEffect(StatusEffectType statusEffectType)
        {
            return TemporaryModifiers[statusEffectType].Invoke();
        }
    }
}