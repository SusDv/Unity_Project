using System.Collections.Generic;
using System.Linq;
using BattleModule.Accuracy.Intervals.SubIntervals.Base;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility;
using UnityEngine;
using Utility;
using Utility.Constants;
using Random = UnityEngine.Random;

namespace BattleModule.Accuracy
{
    public class BattleAccuracy
    {
        private readonly Dictionary<SubIntervalType, SubInterval> _subIntervals = new();
        
        public float HitRate { get; private set; }

        public BattleAccuracy()
        {
            CreateIntervals();
            
            SetupIntervals();
        }

        public BattleAccuracy Init(float accuracy, float evasion, bool isAlly)
        {
            CalculateIntervalRange(accuracy, evasion, isAlly);

            return this;
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

        private void CalculateIntervalRange(float accuracy, float evasion, bool isAlly)
        {
            HitRate = isAlly ? RuntimeConstants.AccuracyConstants.AccuracyMeasureValue : Mathf.RoundToInt(accuracy / (accuracy + evasion) * 100);
            
            foreach (var interval in _subIntervals)
            {
                interval.Value.SetMainIntervalPercentage(HitRate);
            }
        }

        public BattleActionOutcome GetOutcomeByType(SubIntervalType subIntervalType)
        {
            return _subIntervals[subIntervalType].GetBattleActionOutcome();
        }

        public BattleActionOutcome Evaluate()
        {
            int randomValue = Mathf.RoundToInt(Random.Range(0, 100));
            
            return _subIntervals.Values.First(i => 
                i.IntervalRange.IsWithinRange(randomValue))
                .GetBattleActionOutcome();
        }
    }
}