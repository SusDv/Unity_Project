using System.Collections.Generic;
using System.Linq;
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
        public BattleUICharacterInTurn BattleCharacterInTurn;

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
        [SerializeField] private BattleController _battleController;

        private void Awake()
        {
            _playerInventory.InitializeInventory();

            foreach (ItemBase item in testItems)
            {
                _playerInventory.AddItem(item, 2);
            }
        }

        private void Start()
        {
            BattleUIInventory.InitBattleInventory(_playerInventory, _battleController.BattleActionController);
            
            BattleCharacterInTurn.InitCharactersInTurn(
                ref _battleController.BattleCharactersInTurn.OnCharactersInTurnChanged);
            
            BattleUITargeting.InitBattleTrageting(
                ref _battleController.OnCharacterTargetChanged);
            
            BattleUIAction.InitBattleUIAction(_battleController.BattleActionController);

            BattleUIPlayer.InitBattleUICharacter();

            BattleUIEnemy.InitBattleUIEnemy(); 

            BattleUISpells.InitBattleUISpells(
                _battleController.BattleActionController,
                ref _battleController.BattleCharactersInTurn.OnCharactersInTurnChanged);
        }      
    }
}