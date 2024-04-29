using System;
using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Utility;
using CharacterModule.CharacterType.Base;
using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Base;
using UnityEngine;

namespace CharacterModule.Inventory.Items 
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Character/Items/Consumable")]
    public class ConsumableItem : ItemBase, IConsumable, IBattleObject
    {
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
    }
}