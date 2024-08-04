using BattleModule.Actions.Base;
using BattleModule.Utility;
using Utility.Constants;

namespace BattleModule.Actions.Types
{
    public class DefaultAction : BattleAction
    {
        protected override string ActionAnimationName => RuntimeConstants.AnimationConstants.DefaultActionName;
        
        protected override ActionType ActionType => ActionType.DEFAULT;
    }
}
