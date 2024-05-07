using System;

namespace BattleModule.AccuracyModule.Transformer
{
    [Serializable]
    public class StaticOutcomeTransformer : OutcomeTransformer
    {
        protected override void OnTimerExpired()
        {
            BattleTimer.ResetTimer();
            
            base.OnTimerExpired();
        }
    }
}