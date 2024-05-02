using System;
using System.Collections.Generic;
using BattleModule.UI.Presenter.SceneSettings.Action;
using BattleModule.Utility;
using VContainer;

namespace BattleModule.Actions
{
    public class BattleEventManager
    {
        public event Action OnTurnEnded = delegate { };
        
        public event Action OnActionButtonPressed = delegate { };
        
        
        private static readonly List<BattleTimer> BattleTimers = new();
        
        [Inject]
        private BattleEventManager(BattleActionSceneSettings battleActionSceneSettings)
        {
            battleActionSceneSettings.BattleActionButton.OnButtonClick += _ => OnActionButtonPressed.Invoke();
        }
        
        private static void ProcessTimers()
        {
            BattleTimers.RemoveAll(t => t.Expired);
            
            BattleTimers.ForEach(t => t.AdvanceTimer());
        }
        
        public void AdvanceTurn()
        {
            ProcessTimers();
            
            OnTurnEnded?.Invoke();
        }

        public static BattleTimer CreateTimer(int time = 0)
        {
            var battleTimer = new BattleTimer(time);
            
            BattleTimers.Add(battleTimer);
            
            return battleTimer;
        }
    }
}
