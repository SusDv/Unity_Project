using System;

namespace BattleModule.ActionCore.Events
{
    public static class BattleGlobalActionEvent
    {
        public static Action OnBattleAction;

        public static Action OnTurnEnded;

        public static Action OnCycleEnded;

        public static BattleAction BattleAction { get; set; } = BattleDefaultAction.GetBattleDefaultActionInstance();

        private static int MaximumTurnsInCycle;

        private static int TurnsLeft;

        public static void SetMaximumTurnsInCycle(int maximumTurnsInCycle) 
        {
            MaximumTurnsInCycle = TurnsLeft = maximumTurnsInCycle;
        }

        public static void InvokeBattleAction() 
        {
            OnBattleAction?.Invoke();
        }

        public static void AdvanceTurn()
        {
            OnTurnEnded?.Invoke();

            if (--TurnsLeft <= 0)
            {
                AdvanceCycle();
                TurnsLeft = MaximumTurnsInCycle;
            }
            BattleAction = BattleDefaultAction.GetBattleDefaultActionInstance();
        }

        private static void AdvanceCycle()
        {
            OnCycleEnded?.Invoke();
        }
    }
}
