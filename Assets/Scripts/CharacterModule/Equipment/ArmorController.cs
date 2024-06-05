using System.Collections.Generic;
using BattleModule.Actions.Transformer;
using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Equipment;
using CharacterModule.Stats.Managers;
using CharacterModule.Types.Base;
using CharacterModule.Utility;

namespace CharacterModule.Equipment
{
    public class ArmorController
    {
        private readonly Dictionary<ArmorType, IEquipment> _armorList = new ();

        private readonly List<OutcomeTransformer> _outcomeTransformers = new ();
        
        private readonly StatManager _statManager;
        
        public ArmorController(Character belongTo)
        {
            _statManager = belongTo.Stats;
        }

        public void Equip(Armor armor)
        {
            _armorList.TryAdd(armor.ArmorType, armor.GetEquipment());
            
            _armorList[armor.ArmorType].Equip(_statManager);
            
            _outcomeTransformers.AddRange(armor.OutcomeTransformers.GetTransformers());
        }

        public void Unequip(ArmorType armorType)
        {
            if (!_armorList.TryGetValue(armorType, out var armor))
            {
                return;
            }
            
            armor.Unequip(_statManager);

            _armorList.Remove(armorType);
            
            _outcomeTransformers.Clear();
        }

        public void Equip(List<Armor> armorList)
        {
            foreach (var armor in armorList)
            {
                Equip(armor);
            }
        }

        public bool HasArmor(ArmorType armorType)
        {
            return _armorList.TryGetValue(armorType, out _);
        }

        public List<OutcomeTransformer> GetTransformers()
        {
            return _outcomeTransformers;
        }
    }
}