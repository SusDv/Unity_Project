using BattleModule.Accuracy.Intervals.SubIntervals.Base;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility;
using Utility.Constants;

namespace BattleModule.Accuracy.Intervals.SubIntervals
{
    public class HalfDamage : SubInterval
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.HALF;

        public override float SubIntervalPercentage { get; set; } = 0.5f;
        
        public override int Priority => 2;
        
        public override BattleActionOutcome GetBattleActionOutcome()
        {
            return new Actions.BattleActions.Outcome.Outcomes.HalfDamage();
        }

        protected override void UpdateIntervalRange()
        {
            IntervalRange = new IntervalRange(PreviousInterval.IntervalRange.End + 1,(int)(
                PreviousInterval.IntervalRange.End + (RuntimeConstants.AccuracyConstants.AccuracyMeasureValue - HitRate) * SubIntervalPercentage));
        }
    }
}