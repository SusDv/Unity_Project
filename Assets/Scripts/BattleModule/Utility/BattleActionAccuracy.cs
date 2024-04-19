using System.Collections.Generic;
using CharacterModule.Stats.Utility;
using UnityEngine;

namespace BattleModule.Utility
{
    public static class BattleActionAccuracy
    {
        private static readonly Dictionary<int, float> QuarterMultipliers = new()
        {
            {1, 0},
            {2, 0.5f},
            {3, 1}
        };
        
        public static float CalculateAttackDamage(
            StatInfo sourceDamage,
            StatInfo criticalDamage,
            StatInfo targetDefense) 
        {
            return CalculateSourceDamage(sourceDamage.FinalValue, criticalDamage.FinalValue) * (100f / (100f + targetDefense.FinalValue));
        }
        
        private static float CalculateSourceDamage(float sourceDamage, float criticalDamage)
        {
            if (QuarterMultipliers.TryGetValue(GetRandomQuarter(), 
                    out float multiplier))
            {
                return sourceDamage * multiplier;
            }

            return sourceDamage + sourceDamage * (criticalDamage * 0.01f);
        }

        private static int GetRandomQuarter()
        {
            return Mathf.CeilToInt(Random.Range(0f, 1f) * 4f);
        }
    }
}