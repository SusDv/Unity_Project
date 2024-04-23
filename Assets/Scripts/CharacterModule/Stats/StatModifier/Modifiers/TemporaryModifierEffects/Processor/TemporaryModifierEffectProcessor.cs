using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifierEffects.Base;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifierEffects.Processor
{
    public static class TemporaryModifierEffectProcessor
    {
        private static readonly Dictionary<TemporaryEffectType, TemporaryModifierEffect> TemporaryModifiers = new();

        private static bool _initialized;

        private static void Init()
        {
            TemporaryModifiers.Clear();

            var assembly = Assembly.GetAssembly(typeof(TemporaryModifierEffect));

            var temporaryModifierEffects = assembly.GetTypes()
                .Where(t => typeof(TemporaryModifierEffect)
                    .IsAssignableFrom(t) && !t.IsAbstract);

            foreach(var effect in  temporaryModifierEffects) 
            {
                var baseEffect = Activator.CreateInstance(effect) as TemporaryModifierEffect;
                TemporaryModifiers.Add(baseEffect.TemporaryEffectType, baseEffect);
            }

            _initialized = true;
        }

        public static TemporaryModifierEffect GetEffect(TemporaryEffectType temporaryEffectType)
        {
            if (!_initialized)
            {
                Init();
            }

            return TemporaryModifiers[temporaryEffectType];
        }
    }
}