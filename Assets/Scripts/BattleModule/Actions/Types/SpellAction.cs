using BattleModule.Actions.Base;
using Utility.Constants;

namespace BattleModule.Actions.Types
{
    public class SpellAction : BattleAction
    {
        protected override string ActionAnimationName => RuntimeConstants.AnimationConstants.SpellActionName;
    }
}
