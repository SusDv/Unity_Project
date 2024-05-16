using System.Collections.Generic;
using CharacterModule.Inventory.Items;
using CharacterModule.Stats.StatModifier;
using CharacterModule.Stats.StatModifier.Manager;

namespace BattleModule.Actions.BattleActions.Processors
{
    public class ItemActionProcessor : ActionProcessor
    {
        private readonly ConsumableItem _consumableItem;
        
        public ItemActionProcessor(float battlePoints, 
            StatModifiers statModifiers, ConsumableItem consumableItem) 
            : base(battlePoints, statModifiers)
        {
            _consumableItem = consumableItem;
        }

        public override void ApplyModifiers(StatModifierManager source, 
            List<StatModifierManager> targets)
        {
            base.ApplyModifiers(source, targets);
            
            _consumableItem.OnConsumableUsed?.Invoke(_consumableItem);
        }
    }
}