using InventorySystem.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class BattleManager 
        : Singleton<BattleManager>
    {
        [field: SerializeField]
        public List<Character> CharactersToSpawn { get; set; }

        public InventoryBase PlayerInventory { get; set; }
    }
}
