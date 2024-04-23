using BattleModule.Actions.BattleActions.Outcome.Information;
using Utility;

namespace BattleModule.Actions.BattleActions.Outcome.Outcomes
{
    public class BattleActionCrit : BattleActionOutcome
    {
        public BattleActionCrit()
        {
            OutcomeInformation = AssetLoader.Load<OutcomeInformation>("Crit");
        }

        public override float DamageMultiplier => 1f;
    }
}