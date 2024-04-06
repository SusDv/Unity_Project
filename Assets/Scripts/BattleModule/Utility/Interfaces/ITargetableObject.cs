namespace BattleModule.Utility.Interfaces
{
    public interface ITargetableObject
    {
        public TargetType TargetType { get; set; }

        public TargetSearchType TargetSearchType { get; set; }
        
        public int MaxTargetsCount { get; set; }
    }
}
