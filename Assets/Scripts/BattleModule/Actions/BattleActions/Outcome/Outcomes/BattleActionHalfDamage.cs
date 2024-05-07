namespace BattleModule.Actions.BattleActions.Outcome.Outcomes
{
    public class BattleActionHalfDamage : BattleActionOutcome
    {
        public override bool Success => false;

        public override float DamageMultiplier => 0.5f;
    }
}