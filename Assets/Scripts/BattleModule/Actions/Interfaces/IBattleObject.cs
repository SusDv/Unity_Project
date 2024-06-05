using BattleModule.Utility.Interfaces;

namespace BattleModule.Actions.Interfaces
{
    public interface IBattleObject : ITargetableObject
    {
        public float BattlePoints { get; }
    }
}