using UnityEngine;
using Utility.Information;

namespace CharacterModule.Inventory.Items.Base 
{
    public abstract class ItemBase : ScriptableObject, IObjectInformation
    {
        public int ID => GetInstanceID();

        [field: SerializeField] 
        public ObjectInformation ObjectInformation { get; set; }

        [field: SerializeField]
        public bool IsStackable { get; private set; }
    }
}

