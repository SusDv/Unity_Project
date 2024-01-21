using System.ComponentModel;
using StatModule.Utility.Constants;

namespace StatModule.Utility.Enums
{
    public enum StatType
    {
        [Description(StatNames.MAX_HEALTH_NAME)]
        MAX_HEALTH,
        [Description(StatNames.HEALTH_NAME)]
        HEALTH,
        [Description(StatNames.MAX_MANA_NAME)]
        MAX_MANA,
        [Description(StatNames.MANA_NAME)]
        MANA,
        [Description(StatNames.ATTACK_NAME)]
        ATTACK,
        [Description(StatNames.MAGIC_ATTACK_NAME)]
        MAGIC_ATTACK,
        [Description(StatNames.DEFENSE_NAME)]
        DEFENSE,
        [Description(StatNames.MAGIC_DEFENSE_NAME)]
        MAGIC_DEFENSE,
        [Description(StatNames.LUCK_NAME)]
        LUCK,
        [Description(StatNames.CRIT_DAMAGE_NAME)]
        CRIT_DAMAGE,
        [Description(StatNames.CRIT_RATE_NAME)]
        CRIT_RATE,
        [Description(StatNames.EVASION_NAME)]
        EVASION,
        [Description(StatNames.BATTLE_POINTS_NAME)]
        BATTLE_POINTS
    }

    public enum ValueModifierType
    {
        ADDITIVE,
        PERCENTAGE
    }

    public enum TemporaryStatModifierType 
    {
        APPLIED_ONCE,
        APPLIED_EVERY_TURN,
        APPLIED_EVERY_CYCLE
    }
}