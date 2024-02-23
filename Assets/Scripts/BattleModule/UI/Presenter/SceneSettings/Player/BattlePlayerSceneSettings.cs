using System;
using BattleModule.UI.View;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneSettings.Player
{
    [Serializable]
    public class BattlePlayerSceneSettings
    {
        [Header("Panel")]
        [SerializeField] public GameObject BattleUIPlayersPanel;

        [Header("View")]
        [SerializeField] public BattleUIPlayerView BattleUIPlayerView;
    }
}