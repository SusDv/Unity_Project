using BattleModule.Accuracy.Intervals.Utility;
using BattleModule.AccuracyModule.AccuracyRange.Intervals.SubIntervals.Base;
using BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Actions.BattleActions.Outcome.Outcomes;

namespace BattleModule.AccuracyModule.AccuracyRange.Intervals.SubIntervals
{
    public class CritDamage : SubInterval
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.CRIT;

        public override float SubIntervalPercentage { get; set; } = 0.2f;
        
        public override int Priority => 4;
        
        public override BattleActionOutcome GetBattleActionOutcome()
        {
            return new BattleActionCrit();
        }

        protected override void UpdateIntervalRange()
        {
            IntervalRange = new IntervalRange(PreviousInterval.IntervalRange.End, PreviousInterval.IntervalRange.End + HitRate * SubIntervalPercentage);
        }
    }
}