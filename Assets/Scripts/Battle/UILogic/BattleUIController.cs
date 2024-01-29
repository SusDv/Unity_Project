using System.Collections.Generic;
using BattleModule.Controllers.Core;
using BattleModule.UI.Presenter;
using InventorySystem.Core;
using InventorySystem.Item;
using UnityEngine;

namespace BattleModule.UI.Core 
{
    public class BattleUIController : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField]
        private List<ItemBase> testItems;

        [SerializeField]
        private InventoryBase _playerInventory;

        [Header("Inventory Controller")]
        public BattleUIInventory BattleUIInventory;

        [Header("Characters In Turn Controller")]
        public BattleUITurn BattleCharacterInTurn;

        [Header("Battle Action Controller")]
        public BattleUIAction BattleUIAction;

        [Header("Battle Targeting Controller")]
        public BattleUITargeting BattleUITargeting;

        [Header("Battle Player Controller")]
        public BattleUIPlayer BattleUIPlayer;

        [Header("Battle Enemy Controller")]
        public BattleUIEnemy BattleUIEnemy;

        [Header("Battle Spells Controller")]
        public BattleUISpells BattleUISpells;

        [Header("Battle Controller Reference")]
        [SerializeField] private BattleFightController _battleFightController;

        public void Init()
        {
            _playerInventory.InitializeInventory();

            foreach (ItemBase item in testItems)
            {
                _playerInventory.AddItem(item, 2);
            }

            BattleUIInventory.Init(_playerInventory, _battleFightController.BattleActionController);

            BattleCharacterInTurn.Init(_battleFightController.BattleTurnController);

            BattleUITargeting.Init(ref _battleFightController.BattleCharactersOnScene.OnCharacterTargetChanged);

            BattleUIAction.Init(_battleFightController.BattleActionController);

            BattleUIPlayer.Init();

            BattleUIEnemy.Init();

            BattleUISpells.Init(_battleFightController.BattleActionController, _battleFightController.BattleTurnController);

        }      
    }
}