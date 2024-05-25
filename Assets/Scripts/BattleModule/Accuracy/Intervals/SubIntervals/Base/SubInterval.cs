using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility;

namespace BattleModule.Accuracy.Intervals.SubIntervals.Base
{
    public abstract class SubInterval
    {
        protected SubInterval PreviousInterval = null;

        protected float HitRate;
        
        public abstract SubIntervalType SubIntervalType { get; }
        
        public abstract float SubIntervalPercentage { get; set; }

        public abstract int Priority { get; }

        public IntervalRange IntervalRange { get; protected set; }
        
        public abstract BattleActionOutcome GetBattleActionOutcome();

        public void SetPreviousInterval(SubInterval previousInterval)
        {
            PreviousInterval = previousInterval;
        }

        public void SetMainIntervalPercentage(float hitRate)
        {
            HitRate = hitRate;
            
            UpdateIntervalRange();
        }

        protected abstract void UpdateIntervalRange();
    }
}