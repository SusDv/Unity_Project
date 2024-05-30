using BattleModule.Utility;

namespace BattleModule.Actions.BattleActions.Outcome.Outcomes
{
    public class FullDamage : BattleActionOutcome
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.FULL;
        
        public override bool Success => true;
        
        public override float DamageMultiplier => 1f;
    }
}