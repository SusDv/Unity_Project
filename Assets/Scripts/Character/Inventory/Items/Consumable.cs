using BattleModule.Utility.Enums;
using BattleModule.Utility.Interfaces;
using InventorySystem.Item.Interfaces;
using StatModule.Core;
using StatModule.Modifier;
using UnityEngine;

namespace InventorySystem.Item 
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Character/Items/Consumable")]
    public class Consumable : BaseItem, IItemAction, ITargetable
    {
        [field: SerializeField]
        public TargetType TargetType { get; set; }

        [field: SerializeField]
        public TargetSearchType TargetSearchType { get; set; }

        [field: SerializeField]
        [field: Range(1, 3)]
        public int TargetCount { get; set; } = 1;

        public void PerformAction(Character character)
        {
            Stats characterStats = character.GetCharacterStats();

            foreach (BaseStatModifier modifier in StatModifiers.BaseModifiers)
            {
                characterStats.AddStatModifier(modifier.Clone() as BaseStatModifier);
            }

            OnItemAction?.Invoke(this);
        }
    }
}