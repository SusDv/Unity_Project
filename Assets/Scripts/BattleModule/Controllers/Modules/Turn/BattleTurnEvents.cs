using System;

namespace BattleModule.Controllers.Modules.Turn
{
    public class BattleTurnEvents
    {
        public event Action OnBattleInit = delegate { };

        public event Action OnTurnEnd = delegate { };

        public event Action OnActionInvoked = delegate { };

        public void AdvanceTurn()
        {
            OnTurnEnd?.Invoke();
        }

        public void BattleInit()
        {
            OnBattleInit?.Invoke();
        }

        public void InvokeAction()
        {
            OnActionInvoked?.Invoke();
        }
    }
}