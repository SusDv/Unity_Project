using BattleModule.Utility;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace BattleModule.Transition
{
    public class BattleTransitionFlow : IStartable
    {
        private readonly BattleTransitionData _battleTransitionData;

        private BattleTransitionFlow(BattleTransitionData battleTransitionData)
        {
            _battleTransitionData = battleTransitionData;
        }

        public void Start()
        {
            _battleTransitionData.PlayerInventory.InitializeInventory();
            
            foreach (var item in _battleTransitionData.Items)
            {
                _battleTransitionData.PlayerInventory.AddItem(item, 2);
            }
            
            SceneManager.LoadScene("BattleScene");
        }
    }
}