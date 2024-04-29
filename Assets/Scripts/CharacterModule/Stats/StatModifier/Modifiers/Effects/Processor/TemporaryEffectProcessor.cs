using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CharacterModule.Stats.StatModifier.Modifiers.Effects.Base;
using CharacterModule.Stats.Utility.Enums;
using Utility.Types;

namespace CharacterModule.Stats.StatModifier.Modifiers.Effects.Processor
{
    public static class TemporaryEffectProcessor
    {
        private static readonly Dictionary<TemporaryEffectType, Func<TemporaryEffect>> TemporaryModifiers = new()
        {
            { TemporaryEffectType.STATIC_EFFECT , () => new StaticEffect()},
            { TemporaryEffectType.SEAL_EFFECT , () => new SealEffect()},
            { TemporaryEffectType.TIME_EFFECT , () => new TimeEffect()}
        };
        
        public static TemporaryEffect GetEffect(TemporaryEffectType temporaryEffectType)
        {
            return TemporaryModifiers[temporaryEffectType].Invoke();
        }
    }
}