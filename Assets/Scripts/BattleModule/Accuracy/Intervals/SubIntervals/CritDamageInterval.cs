using BattleModule.Accuracy.Intervals.SubIntervals.Base;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Actions.BattleActions.Outcome.Outcomes;
using BattleModule.Utility;

namespace BattleModule.Accuracy.Intervals.SubIntervals
{
    public class CritDamageInterval : SubInterval
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.CRIT;

        public override float SubIntervalPercentage { get; set; } = 0.2f;
        
        public override int Priority => 4;
        
        public override BattleActionOutcome GetBattleActionOutcome()
        {
            return new CritDamage();
        }

        protected override void UpdateIntervalRange()
        {
            IntervalRange = new IntervalRange(PreviousInterval.IntervalRange.End + 1, (int)(PreviousInterval.IntervalRange.End + HitRate * SubIntervalPercentage));
        }
    }
}