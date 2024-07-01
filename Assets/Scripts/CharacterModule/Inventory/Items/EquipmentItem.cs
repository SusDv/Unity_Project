using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Inventory.Processor;
using CharacterModule.Stats.Modifiers.Containers;
using UnityEngine;

namespace CharacterModule.Inventory.Items
{
    public abstract class EquipmentItem : ItemBase, IEquipmentProvider
    {
        [field: SerializeField]
        public EquipmentStatModifiers WearerModifiers { get; private set; }

        public IEquipment GetEquipment()
        {
            return new EquipmentProcessor(ID, WearerModifiers);
        }
    }
}