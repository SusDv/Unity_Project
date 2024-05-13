using System;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneSettings.Turn
{
    [Serializable]
    public class BattleTurnSceneSettings
    {
        [Header("Panel")]
        [SerializeField] public GameObject BattleTurnParent;
    }
}