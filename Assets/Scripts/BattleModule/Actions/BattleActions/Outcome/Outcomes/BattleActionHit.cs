namespace BattleModule.Actions.BattleActions.Outcome.Outcomes
{
    public class BattleActionHit : BattleActionOutcome
    {
        public override bool Success => true;
        
        public override float DamageMultiplier => 1f;
    }
}