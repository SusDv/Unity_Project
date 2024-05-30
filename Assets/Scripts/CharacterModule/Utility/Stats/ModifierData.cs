using System;
using UnityEngine;
using Utility;

namespace CharacterModule.Utility.Stats
{
    [Serializable]
    public class ModifierData
    {
        public ModifierData()
        {
            
        }

        private ModifierData(ValueModifierType valueModifierType,
            ModifiedValueType modifiedValueType,
            float value,
            int sourceID,
            Ref<float> valueToModify)
        {
            ValueModifierType = valueModifierType;
            ModifiedValueType = modifiedValueType;
            Value = value;
            SourceID = sourceID;
            ValueToModify = valueToModify;
        }

        [field: SerializeField]
        public ValueModifierType ValueModifierType { get; private set; }
        
        [field: SerializeField]
        public ModifiedValueType ModifiedValueType { get; private set; }
        
        [field: SerializeField]
        public float Value { get; set; }
        
        public int SourceID { get; set; }

        public Ref<float> ValueToModify { get; set; }

        public ModifierData Clone()
        {
            return new ModifierData(ValueModifierType, ModifiedValueType, Value, SourceID, ValueToModify);
        }
    }
}