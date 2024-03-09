using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Controllers;
using BattleModule.UI.Presenter.SceneSettings.Action;
using CharacterModule;
using Utility;
using VContainer;

namespace BattleModule.Actions
{
    public class BattleEventManager 
        : Singleton<BattleEventManager>
    {
        public event Action OnTurnEnded = delegate { };
        
        public event Action OnCycleEnded = delegate { };

        public event Action OnActionButtonPressed = delegate { };
        

        private int _maximumTurnsInCycle;

        private int _turnsLeft;

        private int _currentTurnCount;

        [Inject]
        private void Init(BattleActionSceneSettings battleActionSceneSettings, BattleSpawner battleSpawner)
        {
            battleSpawner.OnCharactersSpawned += OnCharactersSpawned;
            
            battleActionSceneSettings.BattleActionButton.OnButtonClick += ((o) => OnActionButtonPressed.Invoke());
        }

        private void OnCharactersSpawned(List<Character> characters)
        {
            _maximumTurnsInCycle = _turnsLeft = characters.Count;
        }

        public void AdvanceTurn()
        {
            OnTurnEnded?.Invoke();
            
            _currentTurnCount++;
            
            if (--_turnsLeft > 0)
            {
                return;
            }
            
            AdvanceCycle();

            _turnsLeft = _maximumTurnsInCycle;
        }

        public int GetMaximumTurnsInCycle()
        {
            return _maximumTurnsInCycle;
        }

        public int GetCurrentTurn()
        {
            return _currentTurnCount;
        }

        private void AdvanceCycle()
        {
            OnCycleEnded?.Invoke();
        }
    }
}
