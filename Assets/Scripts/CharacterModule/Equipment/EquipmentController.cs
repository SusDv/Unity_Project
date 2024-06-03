using System;
using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Transformer;
using BattleModule.Utility;
using CharacterModule.Settings;
using CharacterModule.Types.Base;

namespace CharacterModule.Equipment
{
    public class EquipmentController
    {
        private readonly List<OutcomeTransformer> _outcomeTransformers = new ();

        private Func<int, BattleTimer> _battleTimerFactory;
        
        public WeaponController WeaponController { get; private set; }

        public ArmorController ArmorController { get; private set; }

        private void Init(BaseEquipment baseEquipment)
        {
            WeaponController.Equip(baseEquipment.BaseWeapon);
            
            ArmorController.Equip(baseEquipment.BaseArmor);
            
            SetupTransformers();
        }

        private void SetupTransformers()
        {
            foreach (var outcomeTransformer in ArmorController.GetTransformers())
            {
                outcomeTransformer.SetTimer(_battleTimerFactory.Invoke(0));
                
                _outcomeTransformers.Add(outcomeTransformer);
            }
        }

        public EquipmentController(Character belongsTo, BaseEquipment baseEquipment)
        {
            WeaponController = new WeaponController(belongsTo);

            ArmorController = new ArmorController(belongsTo);
            
            Init(baseEquipment);
        }

        public void SetBattleTimerFactory(Func<int, BattleTimer> battleTimerFactory)
        {
            _battleTimerFactory = battleTimerFactory;
        }

        public IEnumerable<OutcomeTransformer> GetTransformers()
        {
            return _outcomeTransformers;
        }

        public void RemoveTemporaryTransformers()
        {
            _outcomeTransformers.RemoveAll(t => t is TemporaryOutcomeTransformer { Duration: <= 0 });
        }
    }
}