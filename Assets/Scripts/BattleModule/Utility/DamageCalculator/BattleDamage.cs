using CharacterModule.Stats.Utility;

namespace BattleModule.Utility.DamageCalculator
{
    public abstract class BattleDamage
    {
        public abstract float CalculateAttackDamage(StatInfo sourceDamage,
            StatInfo criticalDamage,
            StatInfo targetDefense);
    }

    public class PhysicalDamage : BattleDamage
    {
        public override float CalculateAttackDamage(StatInfo sourceDamage, 
            StatInfo criticalDamage, StatInfo targetDefense)
        {
            return sourceDamage.FinalValue * (100f / (100f + targetDefense.FinalValue));
        }
    }

    public class MagicDamage : BattleDamage
    {
        public override float CalculateAttackDamage(StatInfo sourceDamage, StatInfo criticalDamage, StatInfo targetDefense)
        {
            throw new System.NotImplementedException();
        }
    }
}