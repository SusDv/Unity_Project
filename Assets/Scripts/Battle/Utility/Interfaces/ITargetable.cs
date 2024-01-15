using BattleModule.Utility.Enums;

namespace BattleModule.Utility.Interfaces
{
    public interface ITargetable
    {
        public TargetType TargetType { get; set; }

        public TargetSearchType TargetSearchType { get; set; }

        public int TargetCount { get; set; }
    }
}
