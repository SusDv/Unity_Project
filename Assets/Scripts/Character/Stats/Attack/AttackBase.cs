using UnityEngine;

public abstract class AttackBase : ScriptableObject
{
    public abstract float CalculateAttackDamage(Character source, Character target);
}