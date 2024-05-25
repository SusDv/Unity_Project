using System;
using UnityEngine;

namespace BattleModule.Actions.BattleActions.Transformer
{
    [Serializable]
    public class TemporaryOutcomeTransformer : OutcomeTransformer
    {
        [field: SerializeField]
        public int Duration { get; private set; }

        protected override void OnTimerExpired()
        {
            if (--Duration != 0)
            {
                BattleTimer.ResetTimer();
            }
            
            base.OnTimerExpired();
        }
    }
}