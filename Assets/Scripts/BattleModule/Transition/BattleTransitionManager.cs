using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleModule.Transition
{
    public class BattleTransitionManager : MonoBehaviour
    {
        [SerializeField] private BattleTransitionScope _battleTransitionScope;
        
        private void Start()
        {
            _battleTransitionScope.Build();

            SceneManager.LoadScene("BattleScene");
        }
    }
}