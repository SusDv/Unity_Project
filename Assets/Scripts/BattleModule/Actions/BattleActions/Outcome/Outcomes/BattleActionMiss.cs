using BattleModule.Actions.BattleActions.Outcome.Information;
using Utility;

namespace BattleModule.Actions.BattleActions.Outcome.Outcomes
{
    public class BattleActionMiss : BattleActionOutcome
    {
        public BattleActionMiss()
        {
            OutcomeInformation = AssetLoader.Load<OutcomeInformation>("Miss");
        }

        public override float DamageMultiplier => 0f;
    }
}