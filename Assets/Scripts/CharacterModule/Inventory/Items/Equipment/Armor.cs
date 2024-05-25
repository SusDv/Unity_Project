using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Transformer;
using UnityEngine;

namespace CharacterModule.Inventory.Items.Equipment
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Character/Items/Equipment/Armor")]
    public class Armor : EquipmentItem
    {
        [field: SerializeField]
        public ArmorType ArmorType { get; private set; }

        [SerializeField]
        private List<StaticOutcomeTransformer> _staticOutcomeTransformers;
    }

    public enum ArmorType
    {
        CHEST,
        HELMET,
        BOOTS,
        GLOVES
    }
}