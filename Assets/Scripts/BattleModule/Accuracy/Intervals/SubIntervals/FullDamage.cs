using BattleModule.Accuracy.Intervals.SubIntervals.Base;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility;
using Utility.Constants;

namespace BattleModule.Accuracy.Intervals.SubIntervals
{
    public class FullDamage : SubInterval
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.FULL;

        public override float SubIntervalPercentage { get; set; } = 0.8f;
        
        public override int Priority => 3;
        
        public override BattleActionOutcome GetBattleActionOutcome()
        {
            return new Actions.BattleActions.Outcome.Outcomes.FullDamage();
        }

        protected override void UpdateIntervalRange()
        {
            IntervalRange =
                new IntervalRange((int)(RuntimeConstants.AccuracyConstants.AccuracyMeasureValue - HitRate), (int)((RuntimeConstants.AccuracyConstants.AccuracyMeasureValue - HitRate) + HitRate * SubIntervalPercentage) - 1);
        }
    }
}