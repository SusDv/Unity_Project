using System.Collections.Generic;
using BattleModule.Controllers.Base;
using BattleModule.UI.Presenter;
using InventorySystem.Core;
using InventorySystem.Item;
using UnityEngine;

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

        [Header("Battle Fight Controller Reference")]
        [SerializeField] 
        private BattleFightController _battleFightController;

        public void Init()
        {
            _playerInventory.InitializeInventory();

            foreach (ItemBase item in _testItems)
            {
                _playerInventory.AddItem(item, 2);
            }

            _battleUIInventory.Init(_playerInventory, _battleFightController.BattleActionController);

            _battleUITurn.Init(_battleFightController.BattleTurnController);

            _battleUITargeting.Init(ref _battleFightController.BattleTargetingController.OnCharacterTargetChanged);

            _battleUIAction.Init(_battleFightController.BattleActionController);

            _battleUIPlayer.Init();

            _battleUIEnemy.Init();

            _battleUISpells.Init(_battleFightController.BattleActionController, _battleFightController.BattleTurnController);

        }      
    }
}