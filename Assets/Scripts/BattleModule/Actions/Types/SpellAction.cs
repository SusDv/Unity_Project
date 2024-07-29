using BattleModule.Actions.Base;
using BattleModule.Utility;
using Utility.Constants;

namespace BattleModule.Actions.Types
{
    public class SpellAction : BattleAction
    {
        protected override string ActionAnimationName => RuntimeConstants.AnimationConstants.SpellActionName;
        
        protected override ActionType ActionType => ActionType.SPELL;
    }
}
