using BattleModule.Accuracy.Intervals.SubIntervals.Base;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Outcome.Outcomes;
using BattleModule.Utility;
using Utility.Constants;

namespace BattleModule.Accuracy.Intervals.SubIntervals
{
    public class TrueMissInterval : SubInterval
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.MISS;
        
        public override float SubIntervalPercentage { get; set; } = 0.5f;

        public override int Priority => 1;

        public override BattleActionOutcome GetBattleActionOutcome()
        {
            return new TrueMiss();
        }

        protected override void UpdateIntervalRange()
        {
            IntervalRange = new IntervalRange(0,(int)((RuntimeConstants.AccuracyConstants.AccuracyMeasureValue - HitRate) * SubIntervalPercentage - 1));
        }
    }
}