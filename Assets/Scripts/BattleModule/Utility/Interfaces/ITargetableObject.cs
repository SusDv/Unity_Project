namespace BattleModule.Utility.Interfaces
{
    public interface ITargetableObject
    {
        public TargetType TargetType { get; }

        public TargetSearchType TargetSearchType { get; }
        
        public int MaxTargetsCount { get; }
    }
}
