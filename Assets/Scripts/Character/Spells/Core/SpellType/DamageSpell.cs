using UnityEngine;
using SpellModule.Utility;
using System;

namespace SpellModule.Core
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Damage Spell", menuName = "Character/Spells/Damage Spell")]
    public class DamageSpell : SpellBase 
    {
        public override SpellType SpellType => SpellType.DAMAGE;
    }
}
