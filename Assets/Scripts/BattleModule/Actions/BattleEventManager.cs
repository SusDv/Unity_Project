using System;
using BattleModule.Controllers;
using Utils;

namespace BattleModule.Actions
{
    public class BattleEventManager 
        : Singleton<BattleEventManager>
    {
        public event Action OnTurnEnded = delegate { };
        
        public event Action OnCycleEnded = delegate { };

        public Action OnActionButtonPressed = delegate { };
        

        private int _maximumTurnsInCycle;

        private int _turnsLeft;

        private int _currentTurnCount;

        private void Start()
        {
            _maximumTurnsInCycle = _turnsLeft = 6;
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
