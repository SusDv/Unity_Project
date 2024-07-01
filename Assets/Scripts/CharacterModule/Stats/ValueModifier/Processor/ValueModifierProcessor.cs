using System.Collections.Generic;
using CharacterModule.Stats.ValueModifier.Base;
using CharacterModule.Utility;
using CharacterModule.Utility.Stats;
using Utility;

namespace CharacterModule.Stats.ValueModifier.Processor
{
    public static class ValueModifierProcessor
    {
        private static readonly Dictionary<ModifierType, BaseValueModifier> ValueModifiers = new Dictionary<ModifierType, BaseValueModifier>();
        
        private static bool _initialized;

        private static void Init() 
        {
            foreach(var valueModifier in  ReflectionUtils.GetConcreteInstances<BaseValueModifier>()) 
            {
                ValueModifiers.Add(valueModifier.ModifierType, valueModifier);
            }

            _initialized = true;
        }

        public static void ModifyValue(ModifierData data) 
        {
            if (!_initialized)
            {
                Init();
            }

            var valueModifier = ValueModifiers[data.ModifierType];

            valueModifier.ModifyValue(data.ValueToModify, data.Value, data.CalculateFromValue);
        }
    }
}
