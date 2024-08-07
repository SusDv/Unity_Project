using System.Collections.Generic;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Utility;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using Utility;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleTimerController : ILoadingUnit<List<Character>>
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

        public UniTask Load(List<Character> characters)
        {
            _battleTurnEvents.OnTurnEnd += ProcessTimers;
            
            SetLocalCycle(characters.Count);
            
            SetTimerFactory(characters);
            
            return UniTask.CompletedTask;
        }

        private void SetTimerFactory(List<Character> characters)
        {
            foreach (var character in characters)
            {
                character.Stats.SetBattleTimerFactory(CreateTimer);
            }
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