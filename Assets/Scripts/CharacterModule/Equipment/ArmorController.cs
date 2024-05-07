using System.Collections.Generic;
using CharacterModule.CharacterType.Base;
using CharacterModule.Inventory.Items.Equipment;
using CharacterModule.Stats.StatModifier.Manager;

namespace CharacterModule.Equipment
{
    public class ArmorController
    {
        private Dictionary<ArmorType, Armor> _armorList;
        
        private readonly StatModifierManager _statModifierManager;
        
        public ArmorController(Character belongTo)
        {
            _statModifierManager = belongTo.CharacterStats.StatModifierManager;
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