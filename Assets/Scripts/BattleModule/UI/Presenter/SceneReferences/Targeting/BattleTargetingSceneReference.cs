using System;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneReferences.Targeting
{
    [Serializable]
    public class BattleTargetingSceneReference
    {
        [Header("Main Camera Reference")]
        [SerializeField] public Camera MainCamera;
    }
}