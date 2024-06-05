using BattleModule.Actions.BattleActions.Outcome.Outcomes;
using BattleModule.Utility;

namespace BattleModule.Actions.BattleActions.Outcome
{
    public static class BattleOutcomeFactory
    {
        public static BattleActionOutcome GetBattleActionOutcome(SubIntervalType subIntervalType)
        {
            BattleActionOutcome battleActionOutcome = 
                subIntervalType switch
            {
                SubIntervalType.CRIT => new CritDamage(),
                SubIntervalType.FULL => new FullDamage(),
                SubIntervalType.HALF => new HalfDamage(),
                _ => new TrueMiss()
            };

            return battleActionOutcome;
        }
    }
}