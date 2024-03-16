using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.Utility.Enums;
using InventorySystem.Item;
using InventorySystem.Item.Interfaces;
using JetBrains.Annotations;

namespace BattleModule.Actions.BattleActions
{
    [UsedImplicitly]
    public class BattleItemAction : BattleAction 
    {
        protected override string ActionName => "Item use";

        public override void PerformAction(StatManager source, List<Character> targets) 
        {
            var itemToUse = BattleActionContext.ActionObject as ItemBase;
            
            (itemToUse as IConsumable)?.Consume(targets[0].CharacterStats);

            source.ApplyStatModifier(StatType.BATTLE_POINTS, itemToUse.BattlePoints);
        }
    }
}
