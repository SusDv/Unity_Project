using System;
using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.Presenter.SceneSettings.Targeting
{
    [Serializable]
    public class BattleTargetingSceneSettings
    {
        [Header("UI References")]
        [SerializeField] public Canvas BattleTargetingCanvas;
        [SerializeField] public RectTransform TargetGroupPrefab;
        [SerializeField] public Image CharacterTargetImage;

        [Header("Main Camera Reference")]
        [SerializeField] public Camera MainCamera;
    }
}