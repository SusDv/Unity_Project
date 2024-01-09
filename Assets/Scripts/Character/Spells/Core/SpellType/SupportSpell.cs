using UnityEngine;
using SpellModule.Utility;
using System;

namespace SpellModule.Core
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Support Spell", menuName = "Character/Spells/Support Spell")]
    public class SupportSpell : SpellBase
    {
        public override SpellType SpellType => SpellType.SUPPORT;
    }
}
