using System.Collections.Generic;
using BattleModule.AccuracyModule.Transformer;
using CharacterModule.CharacterType.Base;

namespace CharacterModule.Equipment
{
    public class EquipmentController
    {
        private readonly List<OutcomeTransformer> _outcomeTransformers = new ();
        
        public WeaponController WeaponController { get; private set; }

        public ArmorController ArmorController { get; private set; }

        public EquipmentController(Character belongsTo)
        {
            WeaponController = new WeaponController(belongsTo);
        }

        public void RemoveTemporaryTransformers()
        {
            _outcomeTransformers.RemoveAll(t => t is TemporaryOutcomeTransformer { Duration: <= 0 });
        }
    }
}