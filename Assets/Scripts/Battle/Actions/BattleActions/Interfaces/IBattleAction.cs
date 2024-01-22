using System;

namespace BattleModule.ActionCore.Interfaces
{
    public interface IBattleAction
    {
        public BattleAction BattleAction { get; set; }

        public Action OnBattleActionChanged { get; set; }
    }
}
