using System;
using UnityEngine;
using StatModule.Modifier;

namespace InventorySystem.Item 
{
    public abstract class ItemBase : ScriptableObject
    {
        public int ID => GetInstanceID();

        [field: SerializeField]
        public string ItemName { get; private set; }

        [field: SerializeField]
        public Sprite ItemImage { get; private set; }

        [field: SerializeField, TextArea]
        public string ItemDescription { get; private set; }

        [field: SerializeField]
        public bool IsStackable { get; private set; } = false;

        [field: SerializeField]
        public float BattlePoints { get; private set; }

        [field: Header("[Target modifiers]")]
        [field: Space(10f)]

        [field: SerializeField]
        public StatModifiers StatModifiers { get; private set; }

        public Action<ItemBase> OnItemAction;

#if UNITY_EDITOR
        private void OnValidate()
        {
            StatModifiers.BaseModifiers.ForEach(statModifier => statModifier.SourceID = ID);
        }
#else
        private void Awake()
        {
            StatModifiers.BaseModifiers.ForEach(statModifier => statModifier.SourceID = ID);
        }
#endif
        private void OnDisable()
        {
            OnItemAction = null;
        }
    }
}

