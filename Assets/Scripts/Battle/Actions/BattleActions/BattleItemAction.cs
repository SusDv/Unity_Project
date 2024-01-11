using BattleModule.ActionCore.Context;
using InventorySystem.Core;
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
            (((InventoryItem) _battleActionContext.ActionObject).inventoryItem as IItemAction).PerformAction(target);
        }

        public static BattleItemAction GetBattleItemActionInstance(BattleActionContext battleActionContext)
        {
            return new BattleItemAction(battleActionContext);
        }
    }
}
