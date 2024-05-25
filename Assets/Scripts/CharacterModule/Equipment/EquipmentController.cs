using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Transformer;
using CharacterModule.Types.Base;

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

            ArmorController = new ArmorController(belongsTo);
        }

        public void RemoveTemporaryTransformers()
        {
            _outcomeTransformers.RemoveAll(t => t is TemporaryOutcomeTransformer { Duration: <= 0 });
        }
    }
}