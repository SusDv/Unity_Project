using System;
using BattleModule.UI.BattleButton;
using BattleModule.UI.View;
using UnityEngine;

namespace BattleModule.UI.Presenter.Settings.Inventory
{
    [Serializable]
    public class BattleInventorySettings
    {
        [Header("Window")]
        [SerializeField] public GameObject BattleInventoryWindow;
        
        [Header("Item's Parent")]
        [SerializeField] public GameObject BattleInventoryItemsParent;

        [Header("Prefab")]
        [SerializeField] public BattleUIItemView BattleUIItemView;

        [Header("Button")]
        [SerializeField] public BattleUIDefaultButton BattleInventoryButton;

    }
}