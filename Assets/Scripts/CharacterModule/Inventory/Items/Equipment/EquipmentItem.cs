using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Stats.Managers;

namespace CharacterModule.Inventory.Items.Equipment
{
    public abstract class EquipmentItem : ItemBase, IEquipment
    {
        public virtual void Equip(StatManager stats)
        {
            foreach (var baseStatModifier in TargetModifiers.GetModifiers())
            {
                stats.ApplyStatModifier(baseStatModifier);
            }
        }

        public virtual void Unequip(StatManager stats)
        {
            stats.RemoveStatModifiersByCondition((statModifier) => statModifier.SourceID == ID);
        }
    }
}