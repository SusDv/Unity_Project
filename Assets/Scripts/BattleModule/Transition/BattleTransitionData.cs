using System.Collections.Generic;
using CharacterModule;
using CharacterModule.Inventory;
using Utils;
using VContainer;

namespace BattleModule.Transition
{
    public class BattleTransitionData
    {
        public readonly List<Character> CharactersToSpawn;

        public readonly InventoryBase PlayerInventory;

        [Inject]
        public BattleTransitionData(BattleManager battleManager)
        {
            CharactersToSpawn = battleManager.CharactersToSpawn;

            PlayerInventory = battleManager.PlayerInventory;
            
            PlayerInventory.InitializeInventory();

            foreach (var item in battleManager.Items)
            {
                PlayerInventory.AddItem(item, 2);
            }
        }
    }
}