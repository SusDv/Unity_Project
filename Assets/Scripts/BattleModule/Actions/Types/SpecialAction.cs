using BattleModule.Actions.Base;
using BattleModule.Utility;
using Utility.Constants;

namespace BattleModule.Actions.Types
{
    public class SpecialAction : BattleAction
    {
        protected override string ActionAnimationName => RuntimeConstants.AnimationConstants.SpecialActionName;
        
        protected override ActionType ActionType => ActionType.SPECIAL;
    }
}