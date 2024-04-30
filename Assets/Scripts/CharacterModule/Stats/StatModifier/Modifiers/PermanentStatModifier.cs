using System;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class PermanentStatModifier : IStatModifier, IModifier
    {
        private PermanentStatModifier(
            StatType statType,
            ModifierData modifierData)
        {
            StatType = statType;
            
            ModifierData = modifierData;
        }
        
        [field: SerializeField]
        public StatType StatType { get; private set; }
        
        [field: SerializeField]
        public ModifierData ModifierData { get; private set; }
        
        public void OnAdded()
        {
            ValueModifierProcessor.ModifyValue(ModifierData.ValueToModify, this);
        }

        public void OnRemove()
        {
            ValueModifierProcessor.ModifyValue(ModifierData.ValueToModify, this.GetInverseModifier());
        }
        
        public IModifier Clone()
        {
            return new PermanentStatModifier(StatType, ModifierData.Clone());
        }
        
        private bool Equals(PermanentStatModifier other)
        {
            return StatType == other.StatType && Equals(ModifierData, other.ModifierData);
        }

        public bool Equals(IModifier obj)
        {
            return Equals((PermanentStatModifier) obj);
        }
    }
}
