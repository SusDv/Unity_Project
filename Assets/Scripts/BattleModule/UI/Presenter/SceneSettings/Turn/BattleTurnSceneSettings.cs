using System;
using BattleModule.UI.View;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneSettings.Turn
{
    [Serializable]
    public class BattleTurnSceneSettings
    {
        [Header("Panel")]
        [SerializeField] public GameObject BattleTurnParent;

        [Header("View")]
        [SerializeField] public BattleUITurnView BattleUITurnView;
    }
}