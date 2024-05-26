using UnityEngine;

namespace BattleModule.Accuracy.Intervals
{
    public class IntervalRange
    {
        public int Start { get; }

        public int End { get; }
        
        public IntervalRange(int start, int end)
        {
            Start = start;

            End = end;
        }

        private bool IsValidInterval()
        {
            return !(Start == 0 && Mathf.RoundToInt(Start - End) == 0);
        }

        public bool IsWithinRange(int value)
        {
            return IsValidInterval() && (value >= Start && value <= End);
        }
        
        
    }
}