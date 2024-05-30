using BattleModule.Utility;

namespace BattleModule.Actions.BattleActions.Outcome
{
    public abstract class BattleActionOutcome
    {
        public abstract SubIntervalType SubIntervalType { get; }

        public abstract bool Success { get; }

        public abstract float DamageMultiplier { get; }
    }
}