using System.Collections.Generic;
using BattleModule.Scopes;
using InventorySystem.Item;
using UnityEngine;
using Utils;

namespace BattleModule.Boostrap
{
    public class BattleBootstrap : MonoBehaviour
    {
        [SerializeField] private BattleScope _battleScope;
        [SerializeField] private List<ItemBase> _items;
        
        private void Start()
        {
            BattleManager.Instance.PlayerInventory.InitializeInventory();
            
            _items.ForEach(item => BattleManager.Instance.PlayerInventory.AddItem(item, 2));
            
            _battleScope.Build();
        }
    }
}