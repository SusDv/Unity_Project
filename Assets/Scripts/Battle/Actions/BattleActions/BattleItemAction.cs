using InventorySystem.Core;
using InventorySystem.Item.Interfaces;

namespace BattleModule.ActionCore
{
    public class BattleItemAction : BattleAction 
    {
        public override void PerformAction(Character source, Character target) 
        {
            (((InventoryItem) ActionObject).inventoryItem as IItemAction).PerformAction(target);
        }
    }
}
