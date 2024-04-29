using System;
using System.Collections.Generic;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Utility.Enums;
using Utility;

namespace CharacterModule.Stats.Utility
{
    public static class StatModifierExtension
    {
        private static readonly Dictionary<ModifiedValueType, Func<Stat, Ref<float>>> StatModifiedValues = new()
        {
            { ModifiedValueType.FINAL_VALUE, (stat) => new Ref<float>(() => stat.FinalValue, (value) => stat.FinalValue = value) },
            { ModifiedValueType.MAX_VALUE, (stat) => new Ref<float>(() => stat.MaxValue, (value) => stat.MaxValue = value) }
        };
        
        public static void SetValueToModify(this IModifier modifier, Stat stat)
        {
            modifier.ModifierData.ValueToModify =
                StatModifiedValues[modifier.ModifierData.ModifiedValueType].Invoke(stat);
        }

        public static IModifier GetInverseModifier(this IModifier modifier)
        {
            modifier.ModifierData.Value *= -1;

            return modifier;
        }
    }
}