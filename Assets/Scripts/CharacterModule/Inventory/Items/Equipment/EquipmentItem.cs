using System.Collections.Generic;
using BattleModule.AccuracyModule.Transformer;
using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Stats.StatModifier;
using CharacterModule.Stats.StatModifier.Manager;
using UnityEngine;

namespace CharacterModule.Inventory.Items.Equipment
{
    public abstract class EquipmentItem : ItemBase, IEquipment
    {
        [field: SerializeReference]
        public StatModifiers TargetModifiers { get; private set; } = new StaticStatModifiers();

        [field: SerializeField] 
        public List<OutcomeTransformer> OutcomeTransformers { get; private set; }
        
        public virtual void Equip(StatModifierManager stats)
        {
            foreach (var baseStatModifier in TargetModifiers.GetModifiers())
            {
                stats.AddModifier(baseStatModifier);
            }
        }

        public virtual void Unequip(StatModifierManager stats)
        {
            stats.RemoveModifiersOnCondition((statModifier) => statModifier.ModifierData.SourceID == ID);
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            TargetModifiers.GetModifiers().ForEach(statModifier => statModifier.ModifierData.SourceID = ID);
        }
#else
        private void Awake()
        {
            TargetModifiers.GetModifiers().ForEach(statModifier => statModifier.ModifierData.SourceID = ID);
        }
#endif
    }
}