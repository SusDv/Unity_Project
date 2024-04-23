using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BattleModule.Actions.BattleActions.Outcome;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BattleModule.AccuracyModule
{
    public static class BattleAccuracy
    {
        private static readonly Dictionary<int, BattleActionOutcome> ActionOutcomes;

        private static bool _initialized;
        
        public static BattleActionOutcome Evaluate()
        {
            if (!_initialized)
            {
                Init();
            }

            return ActionOutcomes[Mathf.RoundToInt(Random.Range(0f, 1f)) * 4];
        }

        private static void Init()
        {
            ActionOutcomes.Clear();
            
            var assembly = Assembly.GetAssembly(typeof(BattleActionOutcome));

            var battleAccuracyOutcomes = assembly.GetTypes()
                .Where((t) => !t.IsAbstract && typeof(BattleActionOutcome).IsAssignableFrom(t)).ToArray();

            for(var i = 0; i < battleAccuracyOutcomes.Length; i++)
            {
                var battleActionOutcome = Activator.CreateInstance(battleAccuracyOutcomes[i]) as BattleActionOutcome;
                
                ActionOutcomes.Add(i + 1, battleActionOutcome);
            }

            _initialized = true;
        }
    }
}