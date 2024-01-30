using BattleModule.Utility.Enums;

namespace BattleModule.Utility.Interfaces
{
    public interface ITargeting
    {
        public TargetType TargetType { get; set; }

        public TargetSearchType TargetSearchType { get; set; }

        public int MaxTargetsCount { get; set; }
    }
}
