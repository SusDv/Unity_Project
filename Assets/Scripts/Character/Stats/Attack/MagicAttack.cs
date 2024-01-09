using UnityEngine;

[CreateAssetMenu(fileName = "Magic Attack Type", menuName = "Character/Stats/Attack Types/Magic Attack")]
public class MagicAttack : AttackBase
{
    public override float CalculateAttackDamage(Character source, Character target)
    {
        return 0;
    }
}