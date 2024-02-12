using BattleModule.Scopes;
using UnityEngine;

namespace BattleModule.Boostrap
{
    public class BattleBootstrap : MonoBehaviour
    {
        [SerializeField] private BattleScope _battleScope;
        
        private void Start()
        {
            _battleScope.Build();
        }
    }
}