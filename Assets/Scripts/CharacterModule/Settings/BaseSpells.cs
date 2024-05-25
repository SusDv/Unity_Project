using System;
using System.Collections.Generic;
using CharacterModule.Spells.Core;
using UnityEngine;

namespace CharacterModule.Settings
{
    [CreateAssetMenu(fileName = "Base spells", menuName = "Character/Spells/Base/Base Spells")]
    [Serializable]
    public class BaseSpells : ScriptableObject
    {
        [SerializeField] private List<SpellBase> _baseSpells;

        public List<SpellBase> GetSpells() 
        {
            return _baseSpells;
        }
    }
}
