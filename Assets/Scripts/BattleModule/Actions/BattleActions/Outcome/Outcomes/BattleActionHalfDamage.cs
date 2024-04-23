using BattleModule.Actions.BattleActions.Outcome.Information;
using Utility;

namespace BattleModule.Actions.BattleActions.Outcome.Outcomes
{
    public class BattleActionHalfDamage : BattleActionOutcome
    {
        public BattleActionHalfDamage()
        {
            OutcomeInformation = AssetLoader.Load<OutcomeInformation>("Half");
        }

        public override float DamageMultiplier => 0.5f;
    }
}