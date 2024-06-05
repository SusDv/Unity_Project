using BattleModule.Actions.Base;
using Utility.Constants;

namespace BattleModule.Actions.Types
{
    public class SpecialAction : BattleAction
    {
        protected override string ActionAnimationName => RuntimeConstants.AnimationConstants.SpecialActionName;
    }
}