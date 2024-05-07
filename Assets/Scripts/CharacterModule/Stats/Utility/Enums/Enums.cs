using System.ComponentModel;

namespace CharacterModule.Stats.Utility.Enums
{
    public enum StatType
    {
        [Description(StatNames.HealthName)]
        HEALTH,
        [Description(StatNames.ManaName)]
        MANA,
        [Description(StatNames.AttackName)]
        ATTACK,
        [Description(StatNames.MagicAttackName)]
        MAGIC_ATTACK,
        [Description(StatNames.DefenseName)]
        DEFENSE,
        [Description(StatNames.MagicDefenseName)]
        MAGIC_DEFENSE,
        [Description(StatNames.LuckName)]
        LUCK,
        [Description(StatNames.CriticalDamageName)]
        CRITICAL_DAMAGE,
        [Description(StatNames.EvasionName)]
        EVASION,
        [Description(StatNames.AccuracyName)]
        ACCURACY,
        [Description(StatNames.BattlePointsName)]
        BATTLE_POINTS
    }

    public enum ModifiedValueType
    {
        FINAL_VALUE,
        MAX_VALUE
    }

    public enum ValueModifierType
    {
        ADDITIVE,
        PERCENTAGE
    }

    public enum TemporaryEffectType 
    {
        [Description(StatNames.StaticEffectName)]
        STATIC_EFFECT,
        [Description(StatNames.SealEffectName)]
        SEAL_EFFECT,
        [Description(StatNames.TimeEffectName)]
        TIME_EFFECT
    }
}