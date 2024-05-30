using System.Collections.Generic;
using BattleModule.Accuracy;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Actions.BattleActions.Outcome;
using CharacterModule.Types.Base;

namespace BattleModule.Actions.BattleActions.Types
{
    public class DefaultAction : BattleAction
    {
        public override Dictionary<Character, BattleActionOutcome> PerformAction(Character source,
            List<Character> targets, Dictionary<Character, BattleAccuracy> accuracies)
        {
            return base.PerformAction(source, targets, accuracies);
        }
    }
}
