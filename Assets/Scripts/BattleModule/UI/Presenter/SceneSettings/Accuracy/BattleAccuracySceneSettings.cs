using System;
using BattleModule.UI.View;
using UnityEngine;

namespace BattleModule.UI.Presenter.SceneSettings.Accuracy
{
    [Serializable]
    public class BattleAccuracySceneSettings
    {
        public GameObject Parent;
        
        public BattleUIAccuracyView AccuracyViewPrefab;
    }
}