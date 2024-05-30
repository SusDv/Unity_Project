using System.Collections.Generic;
using CharacterModule.Stats.StatModifier.ValueModifier.Base;
using CharacterModule.Utility;
using CharacterModule.Utility.Stats;
using Utility;

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

        public static void ModifyValue(ModifierData data) 
        {
            if (!_initialized)
            {
                Init();
            }

            var valueModifier = ValueModifiers[data.ValueModifierType];

            valueModifier.ModifyValue(data.ValueToModify, data.Value);
        }
    }
}
