using System;
using UnityEngine;

namespace Utility
{
    [Serializable]
    public class ObjectInformation
    {
        [field: SerializeField] 
        public string Name { get; private set; }
        
        [field: SerializeField, TextArea]
        public string Description { get; private set; }
        
        [field: SerializeField] 
        public Sprite Icon { get; private set; }
    }
}