using BattleModule.ActionCore.Context;
using InventorySystem.Core;
using InventorySystem.Item;
using InventorySystem.Item.Interfaces;
using System.Collections.Generic;

namespace BattleModule.ActionCore
{
    public class BattleItemAction : BattleAction 
    {
        private BattleItemAction(BattleActionContext battleActionContext)
            : base(battleActionContext)
        {}

        public override void PerformAction(Character source, List<Character> targets) 
        {
            BaseItem itemToUse = ((InventoryItem)_battleActionContext.ActionObject).inventoryItem;
            (itemToUse as IConsumable).Consume(targets[0]);

            source.GetCharacterStats().AddStatModifier(StatModule.Utility.Enums.StatType.BATTLE_POINTS, itemToUse.BattlePoints);
        }

        public static BattleItemAction GetBattleItemActionInstance(BattleActionContext battleActionContext)
        {
            return new BattleItemAction(battleActionContext);
        }
    }
}
