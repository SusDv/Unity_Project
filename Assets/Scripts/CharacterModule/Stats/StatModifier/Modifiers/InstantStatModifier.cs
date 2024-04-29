using System;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.StatModifier.ValueModifier.Processor;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.StatModifier.Modifiers
{
    [Serializable]
    public class InstantStatModifier : IModifier, IStatModifier
    { 
        private InstantStatModifier(
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
            
        }
        
        public IModifier Clone()
        {
            return new InstantStatModifier(StatType, ModifierData);
        }
        
        public static InstantStatModifier GetInstance(
            StatType statType, 
            float value)
        {
            var modifierData = new ModifierData
            {
                Value = value
            };

            return new InstantStatModifier(statType, modifierData);
        }
    }
}
