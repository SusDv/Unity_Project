using BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility;
using BattleModule.AccuracyModule.Interfaces;
using BattleModule.Actions.BattleActions.Outcome;

namespace BattleModule.AccuracyModule.AccuracyRange.Intervals
{
    public abstract class HitInterval : IAccuracyInterval
    {
        protected float WholeIntervalPercentage;
        
        protected HitInterval(float hitRate)
        {
            WholeIntervalPercentage = hitRate;
        }

        public abstract IntervalType IntervalType { get; }

        public IntervalRange IntervalRange { get; set; }
        
        public abstract float IntervalPercentage { get; protected set; }
        

        public void SetIntervalPercentage(float percentage)
        {
            IntervalPercentage = percentage;
            
            IntervalRange = new IntervalRange(0, IntervalPercentage * WholeIntervalPercentage);
        }

        public abstract BattleActionOutcome GetBattleActionOutcome();
    }
}