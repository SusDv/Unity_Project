using System;
using System.Collections.Generic;
using CharacterModule.Stats.Base;
using Utility;

namespace CharacterModule.Utility.Stats
{
    public static class StatModifierExtension
    {
        private static readonly Dictionary<ModifiedParam, Func<Stat, Ref<float>>> StatModifiedValues = new()
        {
            { ModifiedParam.CURRENT_VALUE, (stat) => new Ref<float>(() => stat.CurrentValue, (value) => stat.CurrentValue = value) },
            { ModifiedParam.MAX_VALUE, (stat) => new Ref<float>(() => stat.MaxValue, (value) => stat.MaxValue = value) }
        };
        
        private static readonly Dictionary<DerivedFrom, Func<Stat, float>> StatModifyFrom = new()
        {
            { DerivedFrom.BASE_VALUE , (stat) => stat.BaseValue},
            { DerivedFrom.CURRENT_VALUE , (stat) => stat.CurrentValue},
            { DerivedFrom.MAX_VALUE , (stat) => stat.MaxValue}
        };
        
        public static void SetupModifierData(this ModifierData modifierData, Stat stat)
        {
            modifierData.ValueToModify =
                StatModifiedValues[modifierData.ModifiedParam].Invoke(stat);

            modifierData.CalculateFromValue = StatModifyFrom[modifierData.DerivedFrom].Invoke(stat);
        }

        public static ModifierData GetInverseModifier(this ModifierData modifierData)
        {
            modifierData.Value *= -1;

            return modifierData;
        }
    }
}