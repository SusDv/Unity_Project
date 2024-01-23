using System;
using Utils;

namespace BattleModule.ActionCore.Events
{
    public class BattleGlobalEventManager 
        : Singleton<BattleGlobalEventManager>
    {
        public Action OnBattleAction = delegate { };

        public Action OnTurnEnded = delegate { };

        public Action OnCycleEnded = delegate { };


        private int MaximumTurnsInCycle;

        private int TurnsLeft;

        public void SetMaximumTurnsInCycle(int maximumTurnsInCycle) 
        {
            MaximumTurnsInCycle = TurnsLeft = maximumTurnsInCycle;
        }

        public void InvokeBattleAction() 
        {
            OnBattleAction?.Invoke();
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
