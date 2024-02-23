using StatModule.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.Modifiers.Base;

namespace StatModule.Modifier.ValueModifier
{
    public static class ValueModifierProcessor
    {
        private static readonly Dictionary<ValueModifierType, ValueModifier> _valueModifiers = new Dictionary<ValueModifierType, ValueModifier>();
        
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
                var baseValueModifier = Activator.CreateInstance(valueModifier) as ValueModifier;
                _valueModifiers.Add(baseValueModifier.ValueModifierType, baseValueModifier);
            }

            _initialized = true;
        }

        public static void ModifyStatValue(Stat statToModify, BaseStatModifier modifier) 
        {
            if (!_initialized)
            {
                Init();
            }

            var valueModifier = _valueModifiers[modifier.ValueModifierType];

            valueModifier.ModifyValue(statToModify, modifier.Value, modifier.ModifierCapType);
        }
    }
}
