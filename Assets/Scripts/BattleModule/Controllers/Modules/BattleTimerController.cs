using System.Collections.Generic;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Utility;
using Cysharp.Threading.Tasks;
using Utility;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleTimerController : ILoadingUnit
    {
        private readonly List<BattleTimer> _battleTimers = new();

        private readonly BattleTurnEvents _battleTurnEvents;

        private int _localCycle;

        [Inject]
        private BattleTimerController(BattleTurnEvents battleTurnEvents)
        {
            _battleTurnEvents = battleTurnEvents;
        }

        private void ProcessTimers()
        {
            _battleTimers.RemoveAll(t => t.Expired);
            
            _battleTimers.ForEach(t => t.AdvanceTimer());
        }

        public UniTask Load()
        {
            _battleTurnEvents.OnTurnEnd += ProcessTimers;
            
            return UniTask.CompletedTask;
        }

        public void SetLocalCycle(int localCycle)
        {
            _localCycle = localCycle;
        }

        public BattleTimer CreateTimer(int time = 0)
        {
            var battleTimer = new BattleTimer(time == 0 ? _localCycle : time);
            
            _battleTimers.Add(battleTimer);
            
            return battleTimer;
        }
    }
}