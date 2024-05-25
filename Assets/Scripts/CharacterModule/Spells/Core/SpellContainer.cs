using System.Collections.Generic;
using CharacterModule.Settings;

namespace CharacterModule.Spells.Core
{
    public class SpellContainer
    {
        private readonly List<SpellBase> _spells;

        public SpellContainer(BaseSpells baseSpells) 
        {
            _spells = new List<SpellBase>(baseSpells.GetSpells());
        }

        public List<SpellBase> GetSpells() 
        {
            return _spells; 
        }
    }
}
