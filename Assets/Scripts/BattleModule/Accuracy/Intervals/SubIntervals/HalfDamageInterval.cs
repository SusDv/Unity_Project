using BattleModule.Accuracy.Intervals.SubIntervals.Base;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Actions.BattleActions.Outcome.Outcomes;
using BattleModule.Utility;
using Utility.Constants;

namespace BattleModule.Accuracy.Intervals.SubIntervals
{
    public class HalfDamageInterval : SubInterval
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.HALF;

        public override float SubIntervalPercentage { get; set; } = 0.5f;
        
        public override int Priority => 2;
        
        public override BattleActionOutcome GetBattleActionOutcome()
        {
            return new HalfDamage();
        }

        protected override void UpdateIntervalRange()
        {
            IntervalRange = new IntervalRange(PreviousInterval.IntervalRange.End + 1,(int)(
                PreviousInterval.IntervalRange.End + (RuntimeConstants.AccuracyConstants.AccuracyMeasureValue - HitRate) * SubIntervalPercentage));
        }
    }
}