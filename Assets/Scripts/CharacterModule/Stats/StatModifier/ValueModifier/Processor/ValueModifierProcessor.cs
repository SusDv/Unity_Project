using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.StatModifier.ValueModifier.Base;
using StatModule.Utility.Enums;
using Utility;

namespace CharacterModule.Stats.StatModifier.ValueModifier.Processor
{
    public static class ValueModifierProcessor
    {
        private static readonly Dictionary<ValueModifierType, BaseValueModifier> _valueModifiers = new Dictionary<ValueModifierType, BaseValueModifier>();
        
        private static bool _initialized;

        private static void Init() 
        {
            _valueModifiers.Clear();

            var assembly = Assembly.GetAssembly(typeof(BaseValueModifier));

            var allValueModifiers = assembly.GetTypes()
                .Where(t => typeof(BaseValueModifier)
                    .IsAssignableFrom(t) && !t.IsAbstract);

            foreach(var valueModifier in  allValueModifiers) 
            {
                var baseValueModifier = Activator.CreateInstance(valueModifier) as BaseValueModifier;
                _valueModifiers.Add(baseValueModifier.ValueModifierType, baseValueModifier);
            }

            _initialized = true;
        }

        public static void ModifyStatValue(Ref<float> valueToModify, BaseStatModifier modifier) 
        {
            if (!_initialized)
            {
                Init();
            }

            var valueModifier = _valueModifiers[modifier.ValueModifierType];

            valueModifier.ModifyValue(valueToModify, modifier.Value);
        }
    }
}
