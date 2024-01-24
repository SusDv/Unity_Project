using SpellModule.Settings;
using System.Collections.Generic;

namespace SpellModule.Base
{
    public class Spells
    {
        private List<SpellBase> _spells;

        public Spells(BaseSpells baseSpells) 
        {
            _spells = new List<SpellBase>(baseSpells.GetSpells());
        }

        public List<SpellBase> GetSpells() 
        {
            return _spells; 
        }
    }
}
