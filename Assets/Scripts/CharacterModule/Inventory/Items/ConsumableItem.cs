using System;
using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Actions.BattleActions.Processors;
using BattleModule.Utility;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Stats.StatModifier;
using UnityEngine;

namespace CharacterModule.Inventory.Items 
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Character/Items/Consumable")]
    public class ConsumableItem : ItemBase, IActionProvider, IBattleObject
    {
        [field: SerializeReference]
        public StatModifiers TargetModifiers { get; private set; } = new DynamicStatModifiers();

        [field: SerializeField]
        public float BattlePoints { get; private set; }
        
        [field: SerializeField]
        public TargetType TargetType { get; private set; }

        public TargetSearchType TargetSearchType => TargetSearchType.SEQUENCE;

        public int MaxTargetsCount => 1;
        
        public Action<ItemBase> OnConsumableUsed;

        public IAction GetAction()
        {
            return new ItemActionProcessor(BattlePoints, TargetModifiers, this);
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            TargetModifiers.GetModifiers().ForEach(statModifier => statModifier.ModifierData.SourceID = ID);
        }
#else
        private void Awake()
        {
            TargetModifiers.GetModifiers().ForEach(statModifier => statModifier.ModifierData.SourceID = ID);
        }
#endif
    }
}