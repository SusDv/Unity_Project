using System.ComponentModel;
using CharacterModule.Utility.Stats;

namespace CharacterModule.Utility
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
        [Description(StatNames.EvasionName)]
        EVASION,
        [Description(StatNames.AccuracyName)]
        ACCURACY,
        [Description(StatNames.BattlePointsName)]
        BATTLE_POINTS
    }

    public enum ModifiedParam
    {
        CURRENT_VALUE,
        MAX_VALUE
    }

    public enum ModifierType
    {
        ADDITIVE,
        PERCENTAGE
    }

    public enum DerivedFrom
    {
        BASE_VALUE,
        CURRENT_VALUE,
        MAX_VALUE
    }

    public enum StatusEffectType 
    {
        [Description(StatNames.StaticEffectName)]
        STATIC_EFFECT,
        [Description(StatNames.SealEffectName)]
        SEAL_EFFECT,
        [Description(StatNames.TimeEffectName)]
        TIME_EFFECT
    }
    
    public enum ArmorType
    {
        CHEST,
        HELMET,
        BOOTS,
        GLOVES
    }
}