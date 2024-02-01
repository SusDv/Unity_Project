using BattleModule.Controllers;
using BattleModule.Controllers.Base;
using BattleModule.UI;
using UnityEngine;

namespace BattleModule.Init
{
    public class BattleInitializer : MonoBehaviour
    {
        [SerializeField] private BattleFightController _battleFightController;
        
        [SerializeField] private BattleUIController _battleUIController;

        private void Awake()
        {
            BattleSpawner.Instance.SpawnCharacters();

            _battleFightController.Init();

            _battleUIController.Init();
        }
    }
}
