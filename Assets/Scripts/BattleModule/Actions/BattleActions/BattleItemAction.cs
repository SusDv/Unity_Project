using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule.CharacterType.Base;
using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Base;

namespace BattleModule.Actions.BattleActions
{
    public class BattleItemAction : BattleAction 
    {
        public override void PerformAction(Character source, List<Character> targets, Action actionFinishedCallback) 
        {
            (BattleActionContext.ActionObject as ItemBase as IConsumable)?.Consume(targets.First());
            
            base.PerformAction(source, targets, actionFinishedCallback);
        }
    }
}
