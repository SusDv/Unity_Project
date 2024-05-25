using BattleModule.Accuracy.Intervals.SubIntervals.Base;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility;

namespace BattleModule.Accuracy.Intervals.SubIntervals
{
    public class TrueMiss : SubInterval
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.MISS;
        
        public override float SubIntervalPercentage { get; set; } = 0.5f;

        public override int Priority => 1;

        public override BattleActionOutcome GetBattleActionOutcome()
        {
            return new Actions.BattleActions.Outcome.Outcomes.TrueMiss();
        }

        protected override void UpdateIntervalRange()
        {
            IntervalRange = new IntervalRange(0f, (1 - HitRate) * SubIntervalPercentage);
        }
    }
}