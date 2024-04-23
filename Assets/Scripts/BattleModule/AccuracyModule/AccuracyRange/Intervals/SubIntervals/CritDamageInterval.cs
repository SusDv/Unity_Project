using BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Actions.BattleActions.Outcome.Outcomes;

namespace BattleModule.AccuracyModule.AccuracyRange.Intervals.SubIntervals
{
    public class CritDamageInterval : HitInterval
    {
        public CritDamageInterval(float hitRate) :
            base(hitRate)
        { }

        public override IntervalType IntervalType => IntervalType.CRIT_DAMAGE;

        public override float IntervalPercentage { get; protected set; } = 0.2f;

        public override BattleActionOutcome GetBattleActionOutcome()
        {
            return new BattleActionCrit();
        }
    }
}