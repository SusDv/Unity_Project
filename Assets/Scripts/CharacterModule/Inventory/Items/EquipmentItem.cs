using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Inventory.Processor;
using CharacterModule.Stats.StatModifier;
using UnityEngine;

namespace CharacterModule.Inventory.Items
{
    public abstract class EquipmentItem : ItemBase, IEquipmentProvider
    {
        [field: SerializeReference]
        public StatModifiers WearerModifiers { get; private set; } = new EquipmentStatModifiers();

        public IEquipment GetEquipment()
        {
            return new EquipmentProcessor(ID, WearerModifiers);
        }
    }
}