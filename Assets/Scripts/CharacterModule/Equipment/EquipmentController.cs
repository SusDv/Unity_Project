using System.Collections.Generic;
using BattleModule.AccuracyModule.Transformer;
using CharacterModule.CharacterType.Base;

namespace CharacterModule.Equipment
{
    public class EquipmentController
    {
        private List<OutcomeTransformer> _outcomeTransformers = new ();
        
        public WeaponController WeaponController { private get; set; }

        public EquipmentController(Character belongsTo)
        {
            WeaponController = new WeaponController(belongsTo);
        }
    }
}