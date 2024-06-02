using System;

namespace BattleModule.Controllers.Modules.Turn
{
    public class BattleTurnEvents
    {
        public event Action OnTurnStart = delegate { };

        public event Action OnTurnEnd = delegate { };

        public event Action OnCycleEnd = delegate { };

        public event Action OnActionInvoked = delegate { };

        public void AdvanceTurn()
        {
            OnTurnEnd?.Invoke();
        }

        public void StartTurn()
        {
            OnTurnStart?.Invoke();
        }

        public void InvokeAction()
        {
            OnActionInvoked?.Invoke();
        }
    }
}