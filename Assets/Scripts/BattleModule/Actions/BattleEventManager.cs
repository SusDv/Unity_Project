using System;
using System.Collections.Generic;
using BattleModule.Controllers;
using BattleModule.UI.Presenter.SceneSettings.Action;
using CharacterModule;
using VContainer;

namespace BattleModule.Actions
{
    public class BattleEventManager
    {
        public event Action OnTurnEnded = delegate { };
        
        public event Action OnActionButtonPressed = delegate { };
        

        private int _maximumTurnsInCycle;

        private int _currentTurnInCycle;

        [Inject]
        public BattleEventManager(BattleActionSceneSettings battleActionSceneSettings, BattleSpawner battleSpawner)
        {
            battleSpawner.OnCharactersSpawned += OnCharactersSpawned;
            
            battleActionSceneSettings.BattleActionButton.OnButtonClick += _ => OnActionButtonPressed.Invoke();
        }

        private void OnCharactersSpawned(List<Character> characters)
        {
            _maximumTurnsInCycle = characters.Count;
        }

        public void AdvanceTurn()
        {
            OnTurnEnded?.Invoke();
            
            _currentTurnInCycle = (_currentTurnInCycle + 1) % _maximumTurnsInCycle;
        }
    }
}
