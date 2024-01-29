using BattleModule.Controllers;
using System;
using Utils;

namespace BattleModule.ActionCore.Events
{
    public class BattleEventManager 
        : Singleton<BattleEventManager>
    {
        public event Action OnTurnEnded = delegate { };

        public event Action OnCycleEnded = delegate { };

        public Action OnActionButtonPressed = delegate { };

        private int MaximumTurnsInCycle;

        private int TurnsLeft;

        private void Start()
        {
            MaximumTurnsInCycle = TurnsLeft = BattleSpawner.Instance.GetSpawnedCharacters().Count;
        }

        public void AdvanceTurn()
        {
            OnTurnEnded?.Invoke();

            if (--TurnsLeft <= 0)
            {
                AdvanceCycle();

                TurnsLeft = MaximumTurnsInCycle;
            }
        }

        private void AdvanceCycle()
        {
            OnCycleEnded?.Invoke();
        }
    }
}
