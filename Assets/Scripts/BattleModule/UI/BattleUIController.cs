using System.Collections.Generic;
using BattleModule.Controllers;
using BattleModule.Controllers.Base;
using BattleModule.Input;
using BattleModule.UI.Presenter;
using InventorySystem.Core;
using InventorySystem.Item;
using UnityEngine;
using VContainer;

namespace BattleModule.UI 
{
    public class BattleUIController : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField]
        private List<ItemBase> _testItems;

        [SerializeField]
        private InventoryBase _playerInventory;
        
        [Header("Battle Inventory Controller")]
        [SerializeField]
        private BattleUIInventory _battleUIInventory;
        
        [Header("Battle Turn Controller")]
        [SerializeField]
        private BattleUITurn _battleUITurn;

        [Header("Battle Action Controller")]
        [SerializeField]
        private BattleUIAction _battleUIAction;

        [Header("Battle Targeting Controller")]
        [SerializeField]
        private BattleUITargeting _battleUITargeting;

        [Header("Battle Player Controller")]
        [SerializeField]
        private BattleUIPlayer _battleUIPlayer;

        [Header("Battle Enemy Controller")]
        [SerializeField]
        private BattleUIEnemy _battleUIEnemy;

        [Header("Battle Spells Controller")]
        [SerializeField]
        private BattleUISpells _battleUISpells;

        [Inject]
        private void Init(BattleSpawner battleSpawner, BattleActionController battleActionController,
            BattleTargetingController battleTargetingController, BattleTurnController battleTurnController)
        {
            _playerInventory.InitializeInventory();

            foreach (var item in _testItems)
            {
                _playerInventory.AddItem(item, 2);
            }

            _battleUIInventory.Init(_playerInventory, battleActionController);

            _battleUITurn.Init(battleTurnController);

            _battleUITargeting.Init(ref battleTargetingController.OnCharacterTargetChanged);

            _battleUIAction.Init(battleActionController);
            
            _battleUIEnemy.Init(battleSpawner);
            
            _battleUIPlayer.Init(battleSpawner);

            _battleUISpells.Init(battleActionController, battleTurnController);

        }
    }
}