using System;
using BattleModule.UI.BattleButton;
using BattleModule.UI.View;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneSettings.Spells
{
    [Serializable]
    public class BattleSpellsSceneSettings
    {
        [Header("Panel")] 
        [SerializeField] 
        public GameObject BattleUISpellsPanel;
        
        [Header("Parent")]
        [SerializeField]
        public GameObject BattleUISpellsParent;

        [Header("View")]
        [SerializeField]
        public BattleUISpellView BattleUISpellView;

        [Header("Button")]
        [SerializeField]
        public BattleUIDefaultButton BattleSpellsMenuButton;
    }
}