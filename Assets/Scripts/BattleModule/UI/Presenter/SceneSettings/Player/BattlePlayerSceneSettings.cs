using System;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneSettings.Player
{
    [Serializable]
    public class BattlePlayerSceneSettings
    {
        [Header("Panel")]
        [SerializeField] public GameObject BattleUIPlayersPanel;
    }
}