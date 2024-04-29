using System.Collections.Generic;
using System.Linq;
using Utility.Types;
using BattleModule.AccuracyModule.AccuracyRange.Intervals.SubIntervals.Base;
using BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility;
using BattleModule.Actions.BattleActions.Outcome;
using Random = UnityEngine.Random;

namespace BattleModule.AccuracyModule
{
    public abstract class Accuracy
    {
        private readonly Dictionary<SubIntervalType, SubInterval> _subIntervals = new();
        
        protected Accuracy()
        {
            CreateIntervals();
            
            SetupIntervals();
        }
        
        private void CreateIntervals()
        {
            foreach (var subInterval in ReflectionUtils.GetConcreteInstances<SubInterval>().OrderBy(i => i.Priority))
            {
                _subIntervals.Add(subInterval.SubIntervalType, subInterval);
            }
        }

        private void SetupIntervals()
        {
            var subIntervalKeys = _subIntervals.Keys.ToList();

            for (var i = 1; i < _subIntervals.Count; i += 2)
            {
                _subIntervals[subIntervalKeys[i]].SetPreviousInterval(
                    _subIntervals[subIntervalKeys[i - 1]]);
            }
        }

        public void CalculateIntervalRange(float accuracy, float evasion)
        {
            float hitRate = accuracy / (accuracy + evasion);
            
            foreach (var interval in _subIntervals)
            {
                interval.Value.SetMainIntervalPercentage(hitRate);
            }
        }

        public BattleActionOutcome Evaluate()
        {
            float randomValue = Random.Range(0, 101) / 100f;
            
            return _subIntervals.First(i => 
                i.Value.IntervalRange.IsWithinRange(randomValue))
                .Value.GetBattleActionOutcome();
        }
    }

    public class UtilityAccuracy : Accuracy
    {

    }

    public class DamageAccuracy : Accuracy
    {
        
    }
}