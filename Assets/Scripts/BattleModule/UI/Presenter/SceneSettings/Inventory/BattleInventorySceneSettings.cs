using System;
using BattleModule.UI.BattleButton;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneSettings.Inventory
{
    [Serializable]
    public class BattleInventorySceneSettings
    {
        [Header("Window")]
        [SerializeField] public GameObject BattleInventoryWindow;
        
        [Header("Item's Parent")]
        [SerializeField] public Transform BattleInventoryItemsParent;
        
        [Header("Button")]
        [SerializeField] public BattleUIDefaultButton BattleInventoryButton;
    }
}