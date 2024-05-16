namespace BattleModule.Actions.BattleActions.Outcome.Outcomes
{
    public class TrueMiss : BattleActionOutcome
    {
        public override bool Success => false;
        
        public override float DamageMultiplier => 0f;
    }
}