using System;
using System.Collections.Generic;
using CharacterModule.Inventory;
using CharacterModule.Inventory.Items.Base;
using UnityEngine;

namespace Utility
{
    public class BattleTransitionData : MonoBehaviour
    {
        [field: SerializeField]
        public List<Player> PlayerCharacters { get; private set; }
        
        [field: SerializeField]
        public List<Enemy> EnemyCharacters { get; private set; }

        [field: SerializeField]
        public InventoryBase PlayerInventory { get; private set; }

        [field: SerializeField]
        public List<ItemBase> Items { get; private set; }

        private void Awake()
        {
            PlayerInventory.InitializeInventory();

            foreach (var item in Items)
            {
                PlayerInventory.AddItem(item, 2);
            }
            
            DontDestroyOnLoad(this);
        }
    }
}
