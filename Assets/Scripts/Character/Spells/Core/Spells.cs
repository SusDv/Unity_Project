using SpellModule.Settings;
using System.Collections.Generic;

namespace SpellModule.Base
{
    public class Spells
    {
        private List<Spell> _spells;

        public Spells(BaseSpells baseSpells) 
        {
            _spells = new List<Spell>(baseSpells.GetSpells());
        }

        public List<Spell> GetSpells() 
        {
            return _spells; 
        }
    }
}
