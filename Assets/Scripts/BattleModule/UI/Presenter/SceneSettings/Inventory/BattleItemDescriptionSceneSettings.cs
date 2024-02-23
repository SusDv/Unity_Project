using System;
using BattleModule.UI.View;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneSettings.Inventory
{
    [Serializable]
    public class BattleItemDescriptionSceneSettings
    {
        [Header("Window")]
        [SerializeField] public GameObject BattleItemDescriptionWindow;
        
        [Header("Prefab")]
        [SerializeField] public BattleUIItemDescriptionView BattleUIItemDescriptionView;

    }
}