using System;
using BattleModule.UI.View;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneSettings.Enemy
{
    [Serializable]
    public class BattleEnemySceneSettings
    {
        [Header("View")]
        [SerializeField] public BattleUIEnemyView BattleUIEnemyView;
    }
}