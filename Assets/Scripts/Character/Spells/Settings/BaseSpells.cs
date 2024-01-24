using SpellModule.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpellModule.Settings
{
    [CreateAssetMenu(fileName = "Base spells", menuName = "Character/Spells/Base Spells")]
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
