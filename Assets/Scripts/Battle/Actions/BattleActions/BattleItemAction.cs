using BattleModule.Utility.Enums;
using InventorySystem.Core;
using InventorySystem.Item.Interfaces;

namespace BattleModule.ActionCore
{
    public class BattleItemAction : BattleAction 
    {
        private BattleItemAction(object actionObject, TargetType targetType)
            : base(actionObject, targetType)
        {}

        public override void PerformAction(Character source, Character target) 
        {
            (((InventoryItem) _actionObject).inventoryItem as IItemAction).PerformAction(target);
        }

        public static BattleItemAction GetBattleItemActionInstance(object actionObject, TargetType targetType)
        {
            return new BattleItemAction(actionObject, targetType);
        }
    }
}
