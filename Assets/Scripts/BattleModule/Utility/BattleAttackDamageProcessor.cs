using UnityEngine;
using StatModule.Utility;
using System.Collections.Generic;

namespace BattleModule.Utility
{
    public static class BattleAttackDamageProcessor
    {
        private static readonly Dictionary<int, float> QuarterMultipliers = new Dictionary<int, float>()
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
            return GetDamage(sourceDamage.FinalValue, criticalDamage.FinalValue) * (100f / (100f + targetDefense.FinalValue));
        }
        
        private static float GetDamage(float sourceDamage, float criticalDamage)
        {
            float randomValue = Random.Range(0f, 1f);

            int quarter = Mathf.CeilToInt(randomValue / 0.25f);

            if (QuarterMultipliers.TryGetValue(quarter, out float multiplier))
            {
                return sourceDamage * multiplier;
            }

            return sourceDamage + sourceDamage * (criticalDamage / 100f);
        }
    }
}