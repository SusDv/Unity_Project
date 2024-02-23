using System;
using BattleModule.Controllers;
using BattleModule.UI.Presenter.SceneSettings.Action;
using Utils;
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
            _maximumTurnsInCycle = _turnsLeft = battleSpawner.GetSpawnedCharacters().Count;
            
            battleActionSceneSettings.BattleActionButton.OnButtonClick += ((o) => OnActionButtonPressed.Invoke());
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
