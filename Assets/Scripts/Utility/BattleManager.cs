using InventorySystem.Core;
using System.Collections.Generic;

namespace Utils
{
    public class BattleManager 
        : Singleton<BattleManager>
    {
        public List<Character> CharactersToSpawn { get; set; }

        public InventorySystemBase PlayerInventory { get; set; }
    }
}
