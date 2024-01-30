using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Base;
using InventorySystem.Item;
using InventorySystem.Item.Interfaces;
using StatModule.Interfaces;

namespace BattleModule.Actions.BattleActions
{
    public class BattleItemAction : BattleAction 
    {
        protected override string ActionName => "Item use";

        public BattleItemAction()
            : base()
        { }

        private BattleItemAction(object actionObject)
            : base(actionObject)
        {}

        public override void PerformAction(IHaveStats source, List<Character> targets) 
        {
            ItemBase itemToUse = BattleActionContext.ActionObject as ItemBase;
            
            (itemToUse as IConsumable).Consume(targets[0].GetCharacterStats());

            source.AddStatModifier(StatModule.Utility.Enums.StatType.BATTLE_POINTS, itemToUse.BattlePoints);
        }

        public override BattleAction GetInstance(object actionObject)
        {
            return new BattleItemAction(actionObject);
        }
    }
}
