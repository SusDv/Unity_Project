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
        public bool IsStackable { get; private set; } = false;

        [field: SerializeField]
        public float BattlePoints { get; private set; }

        [field: Header("[Target modifiers]")]
        [field: Space(10f)]

        [field: SerializeField]
        public StatModifiers StatModifiers { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            StatModifiers.GetModifiers().ForEach(statModifier => statModifier.SourceID = ID);
        }
#else
        private void Awake()
        {
            StatModifiers.GetModifiers().ForEach(statModifier => statModifier.SourceID = ID);
        }
#endif
    }
}

