using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Transformer;
using CharacterModule.Settings;
using CharacterModule.Types.Base;

namespace CharacterModule.Equipment
{
    public class EquipmentController
    {
        private readonly List<OutcomeTransformer> _outcomeTransformers = new ();
        
        public WeaponController WeaponController { get; private set; }

        public ArmorController ArmorController { get; private set; }

        private void Init(BaseEquipment baseEquipment)
        {
            WeaponController.Equip(baseEquipment.BaseWeapon);
            
            ArmorController.Equip(baseEquipment.BaseArmor);
            
            _outcomeTransformers.AddRange(ArmorController.GetTransformers());
        }

        public EquipmentController(Character belongsTo, BaseEquipment baseEquipment)
        {
            WeaponController = new WeaponController(belongsTo);

            ArmorController = new ArmorController(belongsTo);
            
            Init(baseEquipment);
        }

        public List<OutcomeTransformer> GetTransformers()
        {
            return _outcomeTransformers;
        }

        public void RemoveTemporaryTransformers()
        {
            _outcomeTransformers.RemoveAll(t => t is TemporaryOutcomeTransformer { Duration: <= 0 });
        }
    }
}