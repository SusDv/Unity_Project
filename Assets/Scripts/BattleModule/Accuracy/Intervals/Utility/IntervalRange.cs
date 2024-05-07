using UnityEngine;

namespace BattleModule.Accuracy.Intervals.Utility
{
    public class IntervalRange
    {
        public float Start { get; }

        public float End { get; }
        
        public IntervalRange(float start, float end)
        {
            Start = start;

            End = end;
        }

        public bool IsWithinRange(float value)
        {
            return Start == 0 && Mathf.RoundToInt(Start - End) == 0 && (value >= Start && value < End);
        }
    }
}