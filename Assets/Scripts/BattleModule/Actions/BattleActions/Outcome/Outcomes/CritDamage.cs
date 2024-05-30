using BattleModule.Utility;

namespace BattleModule.Actions.BattleActions.Outcome.Outcomes
{
    public class CritDamage : BattleActionOutcome
    {
        public override SubIntervalType SubIntervalType => SubIntervalType.CRIT;
        
        public override bool Success => true;
        
        public override float DamageMultiplier => 2f;
    }
}