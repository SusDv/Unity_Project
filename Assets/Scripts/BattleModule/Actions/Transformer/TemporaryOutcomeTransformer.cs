using System;
using BattleModule.Utility;
using UnityEngine;

namespace BattleModule.Actions.Transformer
{
    [Serializable]
    public class TemporaryOutcomeTransformer : OutcomeTransformer
    {
        [field: SerializeField]
        public int Duration { get; private set; }

        public TemporaryOutcomeTransformer(SubIntervalType from, 
            SubIntervalType to, int cooldown, int duration) 
            : base(from, to, cooldown)
        {
            Duration = duration;
        }
        
        protected override void OnTimerExpired()
        {
            if (--Duration != 0)
            {
                BattleTimer.ResetTimer();
            }
            
            base.OnTimerExpired();
        }
        
        public override OutcomeTransformer Clone()
        {
            return new TemporaryOutcomeTransformer(TransformFrom, TransformTo, Cooldown, Duration);
        }
    }
}