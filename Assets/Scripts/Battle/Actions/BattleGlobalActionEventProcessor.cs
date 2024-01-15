using BattleModule.ActionCore.Context;
using System;

namespace BattleModule.ActionCore.Events
{
    public static class BattleGlobalActionEventProcessor
    {
        public static Action OnBattleAction;

        public static Action OnBattleActionChanged;

        public static Action OnTurnEnded;

        public static Action OnCycleEnded;

        public static BattleAction BattleAction { get; private set; } = 
            BattleDefaultAction.GetBattleDefaultActionInstance(BattleActionContext.GetBattleActionContextInstance(null, Utility.Enums.TargetType.ENEMY));

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

            SetBattleAction(BattleDefaultAction.GetBattleDefaultActionInstance(
                BattleActionContext.GetBattleActionContextInstance(
                    null, Utility.Enums.TargetType.ENEMY)));
        }

        public static void SetBattleAction(BattleAction battleAction) 
        {
            BattleAction = battleAction;
            OnBattleActionChanged?.Invoke();
        }

        private static void AdvanceCycle()
        {
            OnCycleEnded?.Invoke();
        }
    }
}
