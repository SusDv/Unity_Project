using UnityEngine;

[CreateAssetMenu(fileName = "Physic Attack Type", menuName = "Character/Stats/Attack Types/Physic Attack")]
public class PhysicAttack : AttackBase
{
    public override float CalculateAttackDamage(Character source, Character target)
    {
        float physicalDamage = source.GetStats().GetStatFinalValue(StatModule.Utility.Enums.StatType.ATTACK);
        float physicalDefense = target.GetStats().GetStatFinalValue(StatModule.Utility.Enums.StatType.DEFENSE);
        
        return physicalDamage * ((0.006f * physicalDefense) / (1 + 0.006f * physicalDefense));
    }
}