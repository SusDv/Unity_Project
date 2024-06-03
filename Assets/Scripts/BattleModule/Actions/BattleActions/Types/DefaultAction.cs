using BattleModule.Actions.BattleActions.Base;
using Utility.Constants;

namespace BattleModule.Actions.BattleActions.Types
{
    public class DefaultAction : BattleAction
    {
        protected override string ActionAnimationName => RuntimeConstants.AnimationConstants.DefaultActionName;
    }
}
