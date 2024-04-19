using BattleModule.Utility.Interfaces;

namespace BattleModule.Actions.BattleActions.Interfaces
{
    public interface IBattleObject : ITargetableObject
    {
        public float BattlePoints { get; }
    }
}