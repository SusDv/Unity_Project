using System;

namespace BattleModule.Utility
{
    public class BattleTimer
    {
        private readonly int _expireTimeStored;

        private int _expireTime;
        
        public static int LocalCycle { get; set; }

        public bool Started { get; private set; }

        public bool Expired => _expireTime == 0;

        public event Action OnExpired = delegate { };

        public BattleTimer(int expireTime = 0)
        {
            _expireTimeStored = _expireTime = expireTime == 0 ? LocalCycle : expireTime;
        }
        
        public void AdvanceTimer()
        {
            if (!Started)
            {
                return;
            }

            if (--_expireTime != 0)
            {
                return;
            }

            EndTimer();
        }

        public void StartTimer()
        {
            Started = true;
        }

        public void StopTimer()
        {
            Started = false;
        }

        public void EndTimer()
        {
            _expireTime = 0;
            
            StopTimer();
            
            OnExpired?.Invoke();
        }

        public void ResetTimer()
        {
            _expireTime = _expireTimeStored;
            
            StartTimer();
        }
    }
}