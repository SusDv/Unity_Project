using BattleModule.Actions.Transformer.Transformers;
using CharacterModule.Utility;
using UnityEngine;

namespace CharacterModule.Inventory.Items.Equipment
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Character/Items/Equipment/Armor")]
    public class Armor : EquipmentItem
    {
        [field: SerializeField] 
        public StaticTransformers OutcomeTransformers;
        
        [field: SerializeField]
        public ArmorType ArmorType { get; private set; }
    }
}