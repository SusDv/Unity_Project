using BattleModule.Actions.BattleActions.Outcome;
using CharacterModule.Inventory.Items;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier;

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

        public override void ApplyModifiers(StatManager source, 
            StatManager target, 
            BattleActionOutcome battleActionOutcome)
        {
            _consumableItem.OnConsumableUsed?.Invoke(_consumableItem);
            
            AddBattlePoints(source);
            
            if (!battleActionOutcome.Success)
            {
                return;
            }

            ApplyModifiers(target);
            
            ApplyTemporaryModifiers(target);
        }
    }
}