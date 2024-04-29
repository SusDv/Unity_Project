using System.Collections.Generic;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.ValueModifier.Base;
using CharacterModule.Stats.Utility.Enums;
using Utility;
using Utility.Types;

namespace CharacterModule.Stats.StatModifier.ValueModifier.Processor
{
    public static class ValueModifierProcessor
    {
        private static readonly Dictionary<ValueModifierType, BaseValueModifier> ValueModifiers = new Dictionary<ValueModifierType, BaseValueModifier>();
        
        private static bool _initialized;

        private static void Init() 
        {
            foreach(var valueModifier in  ReflectionUtils.GetConcreteInstances<BaseValueModifier>()) 
            {
                ValueModifiers.Add(valueModifier.ValueModifierType, valueModifier);
            }

            _initialized = true;
        }

        public static void ModifyValue(Ref<float> valueToModify, IModifier modifier) 
        {
            if (!_initialized)
            {
                Init();
            }

            var valueModifier = ValueModifiers[modifier.ModifierData.ValueModifierType];

            valueModifier.ModifyValue(valueToModify, modifier.ModifierData.Value);
        }
    }
}
