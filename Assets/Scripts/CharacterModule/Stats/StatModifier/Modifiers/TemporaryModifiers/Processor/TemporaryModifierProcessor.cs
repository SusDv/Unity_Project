using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifiers.Base;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers.TemporaryModifiers
{
    public static class TemporaryModifierProcessor
    {
        private static readonly Dictionary<TemporaryStatModifierType, TemporaryModifier> _temporaryModifiers
            = new Dictionary<TemporaryStatModifierType, TemporaryModifier>();

        private static bool _initialized;

        private static void Init()
        {
            _temporaryModifiers.Clear();

            var assembly = Assembly.GetAssembly(typeof(TemporaryModifier));

            var temporaryModifiers = assembly.GetTypes()
                .Where(t => typeof(TemporaryModifier)
                    .IsAssignableFrom(t) && !t.IsAbstract);

            foreach(var temporaryModifier in  temporaryModifiers) 
            {
                var baseModifier = Activator.CreateInstance(temporaryModifier) as TemporaryModifier;
                _temporaryModifiers.Add(baseModifier.TemporaryStatModifierType, baseModifier);
            }

            _initialized = true;
        }

        public static TemporaryModifier GetModifier(TemporaryStatModifierType temporaryStatModifierType)
        {
            if (!_initialized)
            {
                Init();
            }

            return _temporaryModifiers[temporaryStatModifierType];
        }
    }
}