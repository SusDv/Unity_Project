using System;
using CharacterModule.Stats.Utility.Enums;
using UnityEngine;
using Utility;

namespace CharacterModule.Stats.Utility
{
    [Serializable]
    public class ModifierData
    {
        [field: SerializeField]
        public ValueModifierType ValueModifierType { get; private set; }
        
        [field: SerializeField]
        public ModifiedValueType ModifiedValueType { get; private set; }
        
        [field: SerializeField]
        public float Value { get; set; }
        
        public int SourceID { get; set; }

        public Ref<float> ValueToModify { get; set; }
    }
}