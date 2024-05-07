using BattleModule.Actions.BattleActions.Outcome.Information;

namespace BattleModule.Actions.BattleActions.Outcome
{
    public abstract class BattleActionOutcome
    {
        public OutcomeInformation OutcomeInformation { get; protected set; }

        public abstract bool Success { get; }

        public abstract float DamageMultiplier { get; }
    }
}