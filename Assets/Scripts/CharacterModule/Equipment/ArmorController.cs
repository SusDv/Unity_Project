using System.Collections.Generic;
using CharacterModule.Inventory.Items.Equipment;
using CharacterModule.Stats.Managers;
using CharacterModule.Types.Base;

namespace CharacterModule.Equipment
{
    public class ArmorController
    {
        private Dictionary<ArmorType, Armor> _armorList;
        
        private readonly StatManager _statManager;
        
        public ArmorController(Character belongTo)
        {
            _statManager = belongTo.CharacterStats;
        }

        public void Equip(Armor armor)
        {
            _armorList.TryAdd(armor.ArmorType, armor);
        }
        
        public bool HasArmor(ArmorType armorType)
        {
            return _armorList.TryGetValue(armorType, out _);
        }
    }
}