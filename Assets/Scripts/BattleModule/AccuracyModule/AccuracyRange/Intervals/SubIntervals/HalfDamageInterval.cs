using BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Actions.BattleActions.Outcome.Outcomes;

namespace BattleModule.AccuracyModule.AccuracyRange.Intervals.SubIntervals
{
    public class HalfDamageInterval : HitInterval
    {
        public HalfDamageInterval(float hitRate) :
            base(hitRate)
        { }
        
        public override IntervalType IntervalType => IntervalType.HALF_DAMAGE;

        public override float IntervalPercentage { get; protected set; } = 0.3f;

        public override BattleActionOutcome GetBattleActionOutcome()
        {
            return new BattleActionHalfDamage();
        }
    }
}