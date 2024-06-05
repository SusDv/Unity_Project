using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.Base;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using Utility.Constants;

namespace BattleModule.Actions.Types
{
    public class ItemAction : BattleAction 
    {
        protected override string ActionAnimationName => RuntimeConstants.AnimationConstants.ItemActionName;

        protected override async UniTask PlayActionAnimation(Character source, List<Character> targets)
        {
            await targets.First().AnimationManager.PlayAnimation(ActionAnimationName);
        }
    }
}
