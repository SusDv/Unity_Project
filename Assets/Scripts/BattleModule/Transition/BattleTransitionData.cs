using System.Collections.Generic;
using CharacterModule;
using CharacterModule.Inventory;
using CharacterModule.Inventory.Items.Base;
using UnityEngine;
using Utility;
using VContainer;

namespace BattleModule.Transition
{
    public class BattleTransitionData
    {
        public readonly List<Player> PlayerCharacters;

        public readonly List<Enemy> EnemyCharacters;

        public readonly List<ItemBase> Items;
        
        public BattleTransitionData()
        {
            PlayerCharacters = BattleManager.Instance.PlayerCharacters;
            
            EnemyCharacters = BattleManager.Instance.EnemyCharacters;

            Items = BattleManager.Instance.Items;
        }
    }
}