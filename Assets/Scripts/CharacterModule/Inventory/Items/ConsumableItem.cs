using System;
using BattleModule.Actions.Interfaces;
using BattleModule.Actions.Processors;
using BattleModule.Actions.Transformer.Transformers;
using BattleModule.Utility;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Stats.Modifiers.Containers;
using UnityEngine;

namespace CharacterModule.Inventory.Items 
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Character/Items/Consumable")]
    public class ConsumableItem : ItemBase, IActionProvider, IBattleObject
    {
        [field: SerializeField] 
        private DynamicTransformers _outcomeTransformers;

        [field: SerializeField]
        public DynamicStatModifiers TargetModifiers { get; private set; }

        [field: SerializeField]
        public float BattlePoints { get; private set; }
        
        [field: SerializeField]
        public TargetType TargetType { get; private set; }

        public TargetSearchType TargetSearchType => TargetSearchType.SEQUENCE;

        public int MaxTargetsCount => 1;
        
        public Action<ItemBase> OnConsumableUsed;

        public IAction GetAction()
        {
            return new ItemActionProcessor(ID, TargetModifiers, this, _outcomeTransformers);
        }
    }
}