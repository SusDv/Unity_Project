using System.Collections.Generic;
using CharacterModule.Inventory.Items.Base;
using UnityEngine;

namespace Utility
{
    public class BattleManager 
        : Singleton<BattleManager>
    {
        [field: SerializeField]
        public List<Player> PlayerCharacters { get; set; }
        
        [field: SerializeField]
        public List<Enemy> EnemyCharacters { get; set; }

        public List<ItemBase> Items;
    }
}
