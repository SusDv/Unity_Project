using BattleModule.Utility;

namespace BattleModule.Actions.Outcome.Outcomes
{
    public class TrueMiss : BattleActionOutcome
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.MISS;
        
        public override bool Success => false;
        
        public override float DamageMultiplier => 0f;
    }
}