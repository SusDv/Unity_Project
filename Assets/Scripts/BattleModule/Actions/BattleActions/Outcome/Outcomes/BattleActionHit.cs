using BattleModule.Actions.BattleActions.Outcome.Information;
using Utility;

namespace BattleModule.Actions.BattleActions.Outcome.Outcomes
{
    public class BattleActionHit : BattleActionOutcome
    {
        public BattleActionHit()
        {
            OutcomeInformation = AssetLoader.Load<OutcomeInformation>("Hit");
        }

        public override float DamageMultiplier => 1f;
    }
}