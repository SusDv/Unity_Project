using BattleModule.Accuracy.Intervals.SubIntervals.Base;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility;

namespace BattleModule.Accuracy.Intervals.SubIntervals
{
    public class CritDamage : SubInterval
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.CRIT;

        public override float SubIntervalPercentage { get; set; } = 0.2f;
        
        public override int Priority => 4;
        
        public override BattleActionOutcome GetBattleActionOutcome()
        {
            return new Actions.BattleActions.Outcome.Outcomes.CritDamage();
        }

        protected override void UpdateIntervalRange()
        {
            IntervalRange = new IntervalRange(PreviousInterval.IntervalRange.End, PreviousInterval.IntervalRange.End + HitRate * SubIntervalPercentage);
        }
    }
}