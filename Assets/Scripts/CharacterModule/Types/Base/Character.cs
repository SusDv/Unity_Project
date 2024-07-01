using CharacterModule.Animation;
using CharacterModule.Equipment;
using CharacterModule.Settings;
using CharacterModule.Spells.Core;
using CharacterModule.Stats.Managers;
using CharacterModule.Utility;
using UnityEngine;

namespace CharacterModule.Types.Base
{
    public class Character : MonoBehaviour
    {
        [SerializeField] 
        private BaseSettings _baseSettings;
        
        [field: SerializeField] 
        public AnimationManager AnimationManager { get; private set; }

        [field: SerializeField] 
        public SizeHelper SizeHelper { get; private set; }

        public StatsController Stats { get; private set; }

        public EquipmentController EquipmentController { get; private set; }

        public SpellsController SpellsController { get; private set; }

        public BaseInformation BaseInformation { get; private set; }


        public void Init()
        {
            Stats = new StatsController(_baseSettings.BaseStats);

            SpellsController = new SpellsController(_baseSettings.BaseSpells);

            EquipmentController = new EquipmentController(this, _baseSettings.BaseEquipment);

            BaseInformation = _baseSettings.BaseInformation;
        }
    }
}