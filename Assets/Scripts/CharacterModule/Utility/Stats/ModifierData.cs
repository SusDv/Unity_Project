using System;
using UnityEngine;
using Utility;

namespace CharacterModule.Utility.Stats
{
    [Serializable]
    public class ModifierData
    {
        public ModifierData(float value)
        {
            Value = value;
        }

        private ModifierData(ModifierType modifierType,
            ModifiedParam modifiedParam,
            DerivedFrom derivedFrom,
            float value,
            int sourceID,
            Ref<float> valueToModify)
        {
            ModifierType = modifierType;
            ModifiedParam = modifiedParam;
            Value = value;
            SourceID = sourceID;
            ValueToModify = valueToModify;
        }

        [field: SerializeField]
        public ModifierType ModifierType { get; private set; }
        
        [field: SerializeField]
        public ModifiedParam ModifiedParam { get; private set; }
        
        [field: SerializeField]
        public DerivedFrom DerivedFrom { get; private set; }
        
        [field: SerializeField]
        public float Value { get; set; }

        public float CalculateFromValue { get; set; }

        public int SourceID { get; set; }

        public Ref<float> ValueToModify { get; set; }

        public ModifierData Clone()
        {
            return new ModifierData(ModifierType, 
                ModifiedParam, 
                DerivedFrom, 
                Value, 
                SourceID, 
                ValueToModify);
        }
    }
}