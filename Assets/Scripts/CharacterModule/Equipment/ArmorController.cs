using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Transformer;
using CharacterModule.Inventory.Items.Equipment;
using CharacterModule.Stats.Managers;
using CharacterModule.Types.Base;
using CharacterModule.Utility;

namespace CharacterModule.Equipment
{
    public class ArmorController
    {
        private readonly Dictionary<ArmorType, Armor> _armorList = new ();

        private readonly List<OutcomeTransformer> _outcomeTransformers = new ();
        
        private readonly StatManager _statManager;
        
        public ArmorController(Character belongTo)
        {
            _statManager = belongTo.Stats;
        }

        public void Equip(Armor armor)
        {
            _armorList.TryAdd(armor.ArmorType, armor);
            
            _outcomeTransformers.AddRange(armor.StaticOutcomeTransformers);
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