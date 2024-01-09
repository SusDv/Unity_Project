using System.Collections.Generic;
using System.Linq;
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
        private List<BaseItem> testItems;

        [SerializeField]
        private InventorySystemBase _playerInventory;

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

        [Header("Battle Controller Reference")]
        [SerializeField] private BattleController _battleController;

        private void Awake()
        {
            _playerInventory.InitializeInventory();

            foreach (BaseItem item in testItems)
            {
                _playerInventory.AddItem(item, 2);
            }
        }

        private void Start()
        {
            BattleUIInventory.InitBattleInventory(
                ref _playerInventory.OnInventoryChanged, 
                _playerInventory.GetInventoryItemsByType(typeof(Consumable)));
            
            BattleCharacterInTurn.InitCharactersInTurn(
                ref _battleController.BattleCharactersInTurn.OnCharacterInTurnChanged, 
                _battleController.BattleCharactersInTurn.GetCharactersInTurn().ToList());
            
            BattleUITargeting.InitBattleTrageting(
                ref _battleController.OnCharacterTargetChanged);
            
            BattleUIAction.InitBattleUIAction();

            BattleUIPlayer.InitBattleUICharacter(
                _battleController.BattleCharactersOnScene.GetPlayersOnScene().ToList());
        }      
    }
}