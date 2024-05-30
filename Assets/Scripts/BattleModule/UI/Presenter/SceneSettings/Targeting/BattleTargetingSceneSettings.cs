using System;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneSettings.Targeting
{
    [Serializable]
    public class BattleTargetingSceneSettings
    {
        [Header("Main Camera Reference")]
        [SerializeField] public Camera MainCamera;
    }
}