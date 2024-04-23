using BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Actions.BattleActions.Outcome.Outcomes;

namespace BattleModule.AccuracyModule.AccuracyRange.Intervals.SubIntervals
{
    public class FullDamageInterval : HitInterval
    {
        public FullDamageInterval(float hitRate) :
            base(hitRate)
        { }
        
        public override IntervalType IntervalType => IntervalType.HIT;

        public override float IntervalPercentage { get; protected set; } = 0.5f;
        
        public override BattleActionOutcome GetBattleActionOutcome()
        {
            return new BattleActionHit();
        }
    }
}