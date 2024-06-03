using BattleModule.Actions.BattleActions.Base;
using Utility.Constants;

namespace BattleModule.Actions.BattleActions.Types
{
    public class SpecialAction : BattleAction
    {
        protected override string ActionAnimationName => RuntimeConstants.AnimationConstants.SpecialActionName;
    }
}