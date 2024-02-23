using System;
using BattleModule.UI.BattleButton;
using BattleModule.UI.View;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneSettings.Action
{
    [Serializable]
    public class BattleActionSceneSettings
    {
        [Header("View")]
        [SerializeField] public BattleUIActionView BattleActionView;

        [Header("BattleModule Button")]
        [SerializeField] public BattleUIDefaultButton BattleActionButton;
    }
}