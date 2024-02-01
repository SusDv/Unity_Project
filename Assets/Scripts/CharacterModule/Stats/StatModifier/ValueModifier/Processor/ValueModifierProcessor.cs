using StatModule.Interfaces;
using StatModule.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StatModule.Modifier.ValueModifier
{
    public static class ValueModifierProcessor
    {
        private static Dictionary<ValueModifierType, ValueModifier> _valueModifiers = new Dictionary<ValueModifierType, ValueModifier>();
        
        private static bool _initialized;

        private static void Init() 
        {
            _valueModifiers.Clear();

            var assembly = Assembly.GetAssembly(typeof(ValueModifier));

            var _allValueModifiers = assembly.GetTypes()
                .Where(t => typeof(ValueModifier)
                    .IsAssignableFrom(t) && !t.IsAbstract);

            foreach(var valueModifier in  _allValueModifiers) 
            {
                ValueModifier baseValueModifier = Activator.CreateInstance(valueModifier) as ValueModifier;
                _valueModifiers.Add(baseValueModifier.ValueModifierType, baseValueModifier);
            }

            _initialized = true;
        }

        public static void ModifyStatValue(IStat statToModify, IModifier modifier) 
        {
            if (!_initialized)
            {
                Init();
            }

            var valueModifier = _valueModifiers[modifier.ValueModifierType];

            valueModifier.ModifyValue(statToModify, modifier.Value);
        }
    }
}
