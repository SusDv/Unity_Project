using System.Collections.Generic;
using CharacterModule.Utility;
using CharacterModule.Utility.Stats;

namespace BattleModule.Utility.DamageCalculator
{
    public abstract class BattleDamage
    {
        protected readonly Dictionary<StatType, StatInfo> SourceStats;

        protected float DamageSource;
        
        protected BattleDamage(Dictionary<StatType, StatInfo> sourceStats)
        {
            SourceStats = sourceStats;
        }

        public void SetDamageSource(float damage)
        {
            DamageSource = damage;
        }

        public abstract float CalculateAttackDamage(Dictionary<StatType, StatInfo> targetStats, float multiplier);
    }

    public class PhysicalDamage : BattleDamage
    {
        public PhysicalDamage(Dictionary<StatType, StatInfo> sourceStats)
            : base(sourceStats)
        {
            DamageSource = SourceStats[StatType.ATTACK].FinalValue;
        }
        
        public override float CalculateAttackDamage(Dictionary<StatType, StatInfo> targetStats, float multiplier)
        {
            return -DamageSource * multiplier * (100f / (100f + targetStats[StatType.DEFENSE].FinalValue));
        }
    }

    public class MagicDamage : BattleDamage
    {
        public MagicDamage(Dictionary<StatType, StatInfo> sourceStats)
            : base(sourceStats)
        {
            DamageSource = SourceStats[StatType.MAGIC_ATTACK].FinalValue;
        }
        
        public override float CalculateAttackDamage(Dictionary<StatType, StatInfo> targetStats, float multiplier)
        {
            return -DamageSource * multiplier * (100f / (100f + targetStats[StatType.MAGIC_DEFENSE].FinalValue));
        }
    }
}