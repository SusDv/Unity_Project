using System;
using BattleModule.UI.BattleButton;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneSettings.Action
{
    [Serializable]
    public class BattleActionSceneSettings
    {
        [Header("Battle Button")]
        [SerializeField] public BattleUIDefaultButton BattleActionButton;
    }
}