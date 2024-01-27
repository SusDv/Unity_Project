using BattleModule.ActionCore.Context;
using InventorySystem.Item;
using InventorySystem.Item.Interfaces;
using StatModule.Interfaces;
using System.Collections.Generic;

namespace BattleModule.ActionCore
{
    public class BattleItemAction : BattleAction 
    {
        public override string ActionName => "Item use";

        public BattleItemAction()
            : base()
        { }

        private BattleItemAction(object actionObject)
            : base(actionObject)
        {}

        public override void PerformAction(IHaveStats source, List<Character> targets) 
        {
            ItemBase itemToUse = _battleActionContext.ActionObject as ItemBase;
            
            (itemToUse as IConsumable).Consume(targets[0].GetCharacterStats());

            source.AddStatModifier(StatModule.Utility.Enums.StatType.BATTLE_POINTS, itemToUse.BattlePoints);
        }

        public override BattleAction GetInstance(object actionObject)
        {
            return new BattleItemAction(actionObject);
        }
    }
}
