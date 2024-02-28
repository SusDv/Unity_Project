using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule;
using CharacterModule.Stats.Base;
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
            
            (itemToUse as IConsumable)?.Consume(targets[0].GetCharacterStats());

            source.AddStatModifier(StatModule.Utility.Enums.StatType.BATTLE_POINTS, itemToUse.BattlePoints);
        }
    }
}
