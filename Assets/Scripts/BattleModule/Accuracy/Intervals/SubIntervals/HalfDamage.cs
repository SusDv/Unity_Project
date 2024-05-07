using BattleModule.Accuracy.Intervals.Utility;
using BattleModule.AccuracyModule.AccuracyRange.Intervals.SubIntervals.Base;
using BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Actions.BattleActions.Outcome.Outcomes;

namespace BattleModule.AccuracyModule.AccuracyRange.Intervals.SubIntervals
{
    public class HalfDamage : SubInterval
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.HALF;

        public override float SubIntervalPercentage { get; set; } = 0.5f;
        
        public override int Priority => 2;
        
        public override BattleActionOutcome GetBattleActionOutcome()
        {
            return new BattleActionHalfDamage();
        }

        protected override void UpdateIntervalRange()
        {
            IntervalRange = new IntervalRange(PreviousInterval.IntervalRange.End, 
                PreviousInterval.IntervalRange.End + (1 - HitRate) * SubIntervalPercentage);
        }
    }
}