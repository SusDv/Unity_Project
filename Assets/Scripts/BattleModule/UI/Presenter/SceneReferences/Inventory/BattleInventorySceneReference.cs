using System;
using BattleModule.UI.BattleButton;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneReferences.Inventory
{
    [Serializable]
    public class BattleInventorySceneReference
    {
        [Header("Window")]
        [SerializeField] public GameObject BattleInventoryWindow;
        
        [Header("Item's Parent")]
        [SerializeField] public Transform BattleInventoryItemsParent;
        
        [Header("Button")]
        [SerializeField] public BattleUIButton BattleInventoryButton;
    }
}