using System;
using BattleModule.Utility;

namespace BattleModule.Actions.Transformer
{
    [Serializable]
    public class StaticOutcomeTransformer : OutcomeTransformer
    {
        public StaticOutcomeTransformer(SubIntervalType from, SubIntervalType to, int cooldown) 
            : base(from, to, cooldown)
        { }
        
        protected override void OnTimerExpired()
        {
            BattleTimer.ResetTimer();
            
            base.OnTimerExpired();
        }

        public override OutcomeTransformer Clone()
        {
            return new StaticOutcomeTransformer(TransformFrom, TransformTo, Cooldown);
        }
    }
}