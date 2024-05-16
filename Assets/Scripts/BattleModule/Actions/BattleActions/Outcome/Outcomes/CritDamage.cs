namespace BattleModule.Actions.BattleActions.Outcome.Outcomes
{
    public class CritDamage : BattleActionOutcome
    {
        public override bool Success => true;
        
        public override float DamageMultiplier => 1f;
    }
}