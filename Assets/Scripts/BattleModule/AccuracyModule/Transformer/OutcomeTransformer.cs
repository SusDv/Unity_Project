using System;
using BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility;
using BattleModule.Actions;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility;
using UnityEngine;

namespace BattleModule.AccuracyModule.Transformer
{
    [Serializable]
    public class OutcomeTransformer
    {
        [field: SerializeField]
        public SubIntervalType TransformFrom { get; private set; }
        
        [field: SerializeField]
        public SubIntervalType TransformTo { get; private set; }
        
        [field: SerializeField]
        public int Cooldown { get; private set; }

        [field: NonSerialized]
        public bool IsAvailable { get; private set; } = true;

        public BattleTimer BattleTimer { get; private set; }
        
        public void SetTimer(BattleTimer timer)
        {
            BattleTimer = timer;
        }

        public BattleActionOutcome TransformOutcome(Accuracy accuracy, 
            BattleActionOutcome battleActionOutcome)
        {
            if (accuracy.GetOutcomeByType(TransformFrom).GetType() != battleActionOutcome.GetType()
                || !IsAvailable)
            {
                return battleActionOutcome;
            }

            IsAvailable = false;
            
            BattleTimer.StartTimer();
            
            return accuracy.GetOutcomeByType(TransformTo);
        }

        private void OnTimerExpired()
        {
            IsAvailable = true;
            
            BattleTimer.ResetTimer();
            
            BattleTimer.StopTimer();
        }
    }
}