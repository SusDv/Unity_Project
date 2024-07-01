using System.Collections.Generic;
using CharacterModule.Settings;

namespace CharacterModule.Spells.Core
{
    public class SpellsController
    {
        private readonly List<SpellBase> _spells;

        public SpellsController(BaseSpells baseSpells) 
        {
            _spells = new List<SpellBase>(baseSpells.GetSpells());
        }

        public List<SpellBase> GetSpells() 
        {
            return _spells; 
        }
    }
}
