namespace BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility
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
            return value >= Start && value < End;
        }
    }
}