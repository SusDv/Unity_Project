using System;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility;
using UnityEngine;

namespace BattleModule.Actions.BattleActions.Transformer
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
            SubIntervalType givenType, SubIntervalType expectedType)
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
            if (BattleTimer != null)
            {
                return;
            }

            BattleTimer = timer;
            
            BattleTimer.OnExpired += OnTimerExpired;
        }

        public BattleActionOutcome TransformOutcome(BattleActionOutcome battleActionOutcome)
        {
            if (!IsApplicable(TransformFrom,
                    battleActionOutcome.SubIntervalType))
            {
                return battleActionOutcome;
            }
            
            BattleTimer.StartTimer();
            
            return BattleOutcomeFactory.GetBattleActionOutcome(TransformTo);
        }

        protected virtual void OnTimerExpired()
        {
            IsAvailable = true;
            
            BattleTimer.StopTimer();
        }
    }
}