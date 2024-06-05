using BattleModule.Actions.Base;
using Utility.Constants;

namespace BattleModule.Actions.Types
{
    public class DefaultAction : BattleAction
    {
        protected override string ActionAnimationName => RuntimeConstants.AnimationConstants.DefaultActionName;
    }
}
