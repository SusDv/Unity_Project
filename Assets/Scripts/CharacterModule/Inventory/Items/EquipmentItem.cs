using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Stats.StatModifier;
using CharacterModule.Stats.StatModifier.Manager;
using UnityEngine;

namespace CharacterModule.Inventory.Items
{
    public abstract class EquipmentItem : ItemBase, IEquipment
    {
        [field: SerializeReference]
        public StatModifiers WearerModifiers { get; private set; } = new StaticStatModifiers();
        
        public virtual void Equip(StatModifierManager stats)
        {
            foreach (var baseStatModifier in WearerModifiers.GetModifiers())
            {
                stats.AddModifier(baseStatModifier);
            }
        }

        public virtual void Unequip(StatModifierManager stats)
        {
            stats.RemoveModifiersOnCondition((statModifier) => statModifier.ModifierData.SourceID == ID);
        }
        
        private void Awake()
        {
            WearerModifiers.GetModifiers().ForEach(statModifier => statModifier.ModifierData.SourceID = ID);
        }
    }
}