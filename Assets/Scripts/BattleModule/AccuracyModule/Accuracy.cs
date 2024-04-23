using System.Collections.Generic;
using System.Linq;
using BattleModule.AccuracyModule.AccuracyRange.Intervals;
using BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility;
using BattleModule.AccuracyModule.Interfaces;
using BattleModule.Actions.BattleActions.Outcome;
using UnityEngine;

namespace BattleModule.AccuracyModule
{
    public abstract class Accuracy
    {
        private readonly Dictionary<IntervalType, IAccuracyInterval> _intervalList = new();
        
        private IAccuracyInterval FindIntervalWithinRange(float rangeValue)
        {
            return _intervalList.First(i => i.Value.IntervalRange.IsWithinRange(rangeValue)).Value;
        }
        
        public void SetupIntervals(float accuracy, float evasion)
        {
            float hitRate = accuracy / (accuracy + evasion);
            
            var missInterval = new MissInterval(hitRate);
            
            _intervalList.Add(missInterval.IntervalType, missInterval);
        }

        public BattleActionOutcome Evaluate()
        {
            return FindIntervalWithinRange(Random.Range(1, 100) / 100f)
                .GetBattleActionOutcome();
        }
        
        protected void AddInterval(IAccuracyInterval interval)
        {
            _intervalList.Add(interval.IntervalType, interval);
        }
    }

    public class UtilityAccuracy : Accuracy
    {

    }

    public class DamageAccuracy : Accuracy
    {
        
    }
}