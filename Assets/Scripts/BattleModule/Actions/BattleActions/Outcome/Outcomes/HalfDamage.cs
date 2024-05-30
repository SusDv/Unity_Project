using BattleModule.Utility;

namespace BattleModule.Actions.BattleActions.Outcome.Outcomes
{
    public class HalfDamage : BattleActionOutcome
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.FULL;
        
        public override bool Success => false;

        public override float DamageMultiplier => 0.5f;
    }
}