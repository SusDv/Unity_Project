namespace BattleModule.Utility
{
    public static class BattleAttackDamageProcessor
    {
        public static float CalculateAttackDamage(float damage, float defense) 
        {
            return damage - defense;
        }
    }
}