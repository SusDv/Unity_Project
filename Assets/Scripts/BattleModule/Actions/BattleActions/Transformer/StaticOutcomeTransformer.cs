using System;

namespace BattleModule.Actions.BattleActions.Transformer
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