using System.Collections.Generic;
using BattleModule.AccuracyModule.Transformer;
using UnityEngine;

namespace CharacterModule.Inventory.Items.Equipment
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Character/Items/Equipment/Armor")]
    public class Armor : EquipmentItem, IOutcomeTransformer
    {
        [field: SerializeField]
        public ArmorType ArmorType { get; private set; }

        [SerializeField]
        private List<StaticOutcomeTransformer> _staticOutcomeTransformers;
        
        public List<OutcomeTransformer> GetTransformers()
        {
            return new List<OutcomeTransformer>(_staticOutcomeTransformers);
        }
    }

    public enum ArmorType
    {
        CHEST,
        HELMET,
        BOOTS,
        GLOVES
    }
}