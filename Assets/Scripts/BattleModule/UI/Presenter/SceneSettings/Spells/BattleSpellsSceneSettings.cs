using System;
using BattleModule.UI.BattleButton;
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
        
        [Header("Button")]
        [SerializeField]
        public BattleUIButton BattleSpellsMenuButton;
    }
}