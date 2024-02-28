using InventorySystem.Core;
using System.Collections.Generic;
using CharacterModule;
using CharacterModule.Inventory;
using UnityEngine;

namespace Utils
{
    public class BattleManager 
        : Singleton<BattleManager>
    {
        [field: SerializeField]
        public List<Character> CharactersToSpawn { get; set; }

        [field: SerializeField]
        public InventoryBase PlayerInventory { get; set; }
    }
}
