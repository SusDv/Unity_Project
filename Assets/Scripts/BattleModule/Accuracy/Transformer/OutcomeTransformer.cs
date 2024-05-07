using System;
using BattleModule.Accuracy;
using BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility;
using UnityEngine;

namespace BattleModule.AccuracyModule.Transformer
{
    [Serializable]
    public abstract class OutcomeTransformer
    {
        [field: SerializeField]
        public SubIntervalType TransformFrom { get; protected set; }
        
        [field: SerializeField]
        public SubIntervalType TransformTo { get; protected set; }
        
        [field: SerializeField]
        public int Cooldown { get; protected set; }

        [field: NonSerialized]
        public bool IsAvailable { get; protected set; } = true;

        public BattleTimer BattleTimer { get; protected set; }

        private bool IsApplicable(
            Type givenType, Type expectedType)
        {
            if (givenType != expectedType
                 || !IsAvailable)
            {
                return false;
            }

            IsAvailable = false;
            
            return true;
        }

        public void SetTimer(BattleTimer timer)
        {
            BattleTimer = timer;
            
            BattleTimer.OnExpired += OnTimerExpired;
        }

        public BattleActionOutcome TransformOutcome(BattleAccuracy battleAccuracy,
            BattleActionOutcome battleActionOutcome)
        {
            if (!IsApplicable(battleAccuracy.GetOutcomeByType(TransformFrom).GetType(),
                    battleActionOutcome.GetType()))
            {
                return battleActionOutcome;
            }
            
            BattleTimer.StartTimer();
            
            return battleAccuracy.GetOutcomeByType(TransformTo);
        }

        protected virtual void OnTimerExpired()
        {
            IsAvailable = true;
            
            BattleTimer.StopTimer();
        }
    }
}