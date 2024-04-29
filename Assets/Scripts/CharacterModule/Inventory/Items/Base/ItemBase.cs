using CharacterModule.Stats.StatModifier;
using UnityEngine;
using Utility;
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

        [field: SerializeField]
        public StatModifiers TargetModifiers { get; private set; }

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

