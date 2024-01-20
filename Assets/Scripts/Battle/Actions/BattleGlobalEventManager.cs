using System;
using UnityEngine;

namespace BattleModule.ActionCore.Events
{
    public class BattleGlobalEventManager : MonoBehaviour
    {
        private static BattleGlobalEventManager _instance;

        public static BattleGlobalEventManager Instance 
        { 
            get 
            {
                if (_instance == null) 
                {
                    _instance = FindFirstObjectByType<BattleGlobalEventManager>();
                }

                return _instance;
            } 
        }

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

        private void OnDisable()
        {
            _instance = null;
        }
    }
}
