using BattleModule.ActionCore.Context;
using InventorySystem.Core;
using InventorySystem.Item;
using InventorySystem.Item.Interfaces;

namespace BattleModule.ActionCore
{
    public class BattleItemAction : BattleAction 
    {
        private BattleItemAction(BattleActionContext battleActionContext)
            : base(battleActionContext)
        {}

        public override void PerformAction(Character source, Character target) 
        {
            BaseItem itemToUse = ((InventoryItem)_battleActionContext.ActionObject).inventoryItem;
            (itemToUse as IItemAction).PerformAction(target);

            source.GetCharacterStats().AddStatModifier(StatModule.Utility.Enums.StatType.BATTLE_POINTS, itemToUse.BattlePoints);
        }

        public static BattleItemAction GetBattleItemActionInstance(BattleActionContext battleActionContext)
        {
            return new BattleItemAction(battleActionContext);
        }
    }
}
