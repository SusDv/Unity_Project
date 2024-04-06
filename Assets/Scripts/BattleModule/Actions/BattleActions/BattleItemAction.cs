using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Actions.BattleActions.Context;
using CharacterModule;
using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.Utility.Enums;
using JetBrains.Annotations;

namespace BattleModule.Actions.BattleActions
{
    [UsedImplicitly]
    public class BattleItemAction : BattleAction 
    {
        public override void PerformAction(StatManager source, List<Character> targets) 
        {
            var itemToUse = BattleActionContext.ActionObject as ItemBase;
            
            (itemToUse as IConsumable)?.Consume(targets.First().CharacterStats);

            source.ApplyStatModifier(StatType.BATTLE_POINTS, itemToUse.BattlePoints);
        }
    }
}
