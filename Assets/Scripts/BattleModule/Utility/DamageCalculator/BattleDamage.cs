using CharacterModule.Stats.Managers;
using CharacterModule.Utility;

namespace BattleModule.Utility.DamageCalculator
{
    public abstract class BattleDamage
    {
        protected readonly StatsController SourceStats;

        protected float DamageSource;
        
        protected BattleDamage(StatsController source)
        {
            SourceStats = source;
        }

        public void SetDamageSource(float damage)
        {
            DamageSource = damage;
        }

        public abstract float CalculateAttackDamage(StatsController target, float multiplier);
    }

    public class PhysicalDamage : BattleDamage
    {
        public PhysicalDamage(StatsController source)
            : base(source)
        {
            DamageSource = SourceStats.GetStatInfo(StatType.ATTACK).FinalValue;
        }
        
        public override float CalculateAttackDamage(StatsController target, float multiplier)
        {
            return -DamageSource * multiplier * (100f / (100f + target.GetStatInfo(StatType.DEFENSE).FinalValue));
        }
    }

    public class MagicDamage : BattleDamage
    {
        public MagicDamage(StatsController source)
            : base(source)
        {
            DamageSource = SourceStats.GetStatInfo(StatType.MAGIC_ATTACK).FinalValue;
        }
        
        public override float CalculateAttackDamage(StatsController target, float multiplier)
        {
            return -DamageSource * multiplier * (100f / (100f + target.GetStatInfo(StatType.MAGIC_DEFENSE).FinalValue));
        }
    }
}