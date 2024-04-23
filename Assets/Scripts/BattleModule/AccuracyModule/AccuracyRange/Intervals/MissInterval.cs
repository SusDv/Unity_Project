using BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility;
using BattleModule.AccuracyModule.Interfaces;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Actions.BattleActions.Outcome.Outcomes;

namespace BattleModule.AccuracyModule.AccuracyRange.Intervals
{
    public class MissInterval : IAccuracyInterval
    {
        public MissInterval(float hitRate)
        {
            IntervalPercentage = 1 - hitRate;

            SetIntervalPercentage(IntervalPercentage);
        }

        public IntervalType IntervalType => IntervalType.MISS;
        
        public IntervalRange IntervalRange { get; set; }

        public float IntervalPercentage { get; set; }
        
        public void SetIntervalPercentage(float intervalPercentage)
        {
            IntervalRange = new IntervalRange(0f, IntervalPercentage);
        }

        public BattleActionOutcome GetBattleActionOutcome()
        {
            return new BattleActionMiss();
        }
    }
}