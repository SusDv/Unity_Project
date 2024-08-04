using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.Base;
using BattleModule.Utility;
using CharacterModule.Animation;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using Utility.Constants;

namespace BattleModule.Actions.Types
{
    public class ItemAction : BattleAction 
    {
        protected override string ActionAnimationName => RuntimeConstants.AnimationConstants.ItemActionName;

        protected override ActionType ActionType => ActionType.ITEM;
        
        protected override async UniTask<bool> PlayActionAnimation(AnimationManager source, IEnumerable<Character> targets, Action triggerCallback)
        {
            return await targets.First().AnimationManager.PlayAnimation(ActionAnimationName, triggerCallback);
        }
    }
}
