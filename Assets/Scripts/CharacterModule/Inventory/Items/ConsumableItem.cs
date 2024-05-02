using System;
using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Utility;
using CharacterModule.CharacterType.Base;
using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Stats.StatModifier;
using UnityEngine;

namespace CharacterModule.Inventory.Items 
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Character/Items/Consumable")]
    public class ConsumableItem : ItemBase, IConsumable, IBattleObject
    {
        [field: SerializeReference]
        public StatModifiers TargetModifiers { get; private set; } = new DynamicStatModifiers();

        [field: SerializeField]
        public float BattlePoints { get; private set; }
        
        [field: SerializeField]
        public TargetType TargetType { get; private set; }

        public TargetSearchType TargetSearchType => TargetSearchType.SEQUENCE;

        public int MaxTargetsCount => 1;
        

        public event Action<ItemBase> OnConsumableUsed;

        public void Consume(Character target)
        {
            foreach (var modifier in TargetModifiers.GetModifiers())
            {
                target.CharacterStats.StatModifierManager.AddModifier(modifier);
            }

            OnConsumableUsed?.Invoke(this);
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