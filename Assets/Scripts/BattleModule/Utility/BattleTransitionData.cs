using System.Collections.Generic;
using CharacterModule.Inventory;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Types;
using UnityEngine;

namespace BattleModule.Utility
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
    }
}
