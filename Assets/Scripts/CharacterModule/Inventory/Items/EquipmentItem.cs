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
        public StatModifiers WearerModifiers { get; private set; } = new StaticStatModifiers();

        public IEquipment GetEquipment()
        {
            return new EquipmentProcessor(ID, WearerModifiers);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            WearerModifiers.SetSourceID(GetInstanceID());
        }
#else
        private void Awake()
        {
            WearerModifiers.SetSourceID(GetInstanceID());
        }
#endif
    }
}