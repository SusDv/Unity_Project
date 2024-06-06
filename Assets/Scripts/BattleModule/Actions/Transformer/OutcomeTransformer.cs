using System;
using BattleModule.Actions.Outcome;
using BattleModule.Utility;
using UnityEngine;

namespace BattleModule.Actions.Transformer
{
    [Serializable]
    public abstract class OutcomeTransformer : IEquatable<OutcomeTransformer>
    {
        [field: SerializeField]
        public SubIntervalType TransformFrom { get; private set; }
        
        [field: SerializeField]
        public SubIntervalType TransformTo { get; private set; }
        
        [field: SerializeField]
        public int Cooldown { get; protected set; }

        [field: NonSerialized]
        public bool IsAvailable { get; protected set; } = true;

        [field: NonSerialized] 
        public bool Initialized { get; protected set; }

        protected BattleTimer BattleTimer { get; set; }

        private Func<int, BattleTimer> _battleTimerFactory;

        protected OutcomeTransformer(SubIntervalType from,
            SubIntervalType to, int cooldown)
        {
            TransformFrom = from;
            
            TransformTo = to;
            
            Cooldown = cooldown;
        }

        public bool IsApplicable(SubIntervalType givenType)
        {
            if (givenType != TransformFrom || !IsAvailable)
            {
                return false;
            }

            IsAvailable = false;
            
            return true;
        }

        private void SetupTimer()
        {
            BattleTimer = _battleTimerFactory.Invoke(0);
            
            BattleTimer.OnExpired += OnTimerExpired;
            
            BattleTimer.StartTimer();
        }

        public void SetTimerFactory(Func<int, BattleTimer> timerFactory)
        {
            if (Initialized)
            {
                return;
            }
            
            _battleTimerFactory = timerFactory;
            
            Initialized = true;
        }

        public SubIntervalType GetTransformTo(BattleActionOutcome battleActionOutcome)
        {
            if (!IsApplicable(battleActionOutcome.SubIntervalType))
            {
                return battleActionOutcome.SubIntervalType;
            }
            
            SetupTimer();
            
            return TransformTo;
        }

        public abstract OutcomeTransformer Clone();

        protected virtual void OnTimerExpired()
        {
            IsAvailable = true;
            
            BattleTimer.StopTimer();
        }

        public bool Equals(OutcomeTransformer other)
        {
            return other != null
                   && TransformFrom == other.TransformFrom
                   && TransformTo == other.TransformTo;
        }
    }
}