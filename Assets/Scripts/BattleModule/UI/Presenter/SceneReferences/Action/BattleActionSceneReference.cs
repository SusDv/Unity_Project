using System;
using BattleModule.UI.BattleButton;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneReferences.Action
{
    [Serializable]
    public class BattleActionSceneReference
    {
        [Header("Battle Button")]
        [SerializeField] public BattleUIActionButton BattleActionButton;
    }
}