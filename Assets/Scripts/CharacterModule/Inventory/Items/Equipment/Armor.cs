using CharacterModule.Utility;
using UnityEngine;

namespace CharacterModule.Inventory.Items.Equipment
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Character/Items/Equipment/Armor")]
    public class Armor : EquipmentItem
    {
        [field: SerializeField]
        public ArmorType ArmorType { get; private set; }
    }
}