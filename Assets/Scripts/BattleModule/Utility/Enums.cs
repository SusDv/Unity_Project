namespace BattleModule.Utility
{
    public enum TargetType 
    {
        ALLY,
        ENEMY
    }

    public enum ActionType
    {
        ITEM,
        SPELL,
        DEFAULT,
        SPECIAL
    }

    public enum TargetSearchType 
    {
        MULTIPLE_TARGET,
        SEQUENCE
    }
    
    public enum SubIntervalType
    {
        MISS,
        HALF,
        FULL,
        CRIT
    }
}
