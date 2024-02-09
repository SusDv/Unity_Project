using System;
using BattleModule.UI.View;
using UnityEngine;

namespace BattleModule.UI.Presenter.Settings.Inventory
{
    [Serializable]
    public class BattleItemDescriptionSettings
    {
        [Header("Window")]
        [SerializeField] public GameObject BattleItemDescriptionWindow;
        
        [Header("Prefab")]
        [SerializeField] public BattleUIItemDescriptionView BattleUIItemDescriptionView;

    }
}