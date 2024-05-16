using System;

namespace BattleModule.Accuracy.Transformer
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