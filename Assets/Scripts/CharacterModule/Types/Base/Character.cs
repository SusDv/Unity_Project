using System;
using CharacterModule.Animation;
using CharacterModule.Data.Info;
using CharacterModule.Equipment;
using CharacterModule.Settings;
using CharacterModule.Spells.Core;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.Managers.SingleStat;
using UnityEngine;

namespace CharacterModule.Types.Base
{
    public class Character : MonoBehaviour, IDisposable
    {
        [SerializeField] 
        private CharacterSettings _characterSettings;
        
        [field: SerializeField] 
        public AnimationManager AnimationManager { get; private set; }
        
        public StatManager CharacterStats { get; private set; }

        public EquipmentController EquipmentController { get; private set; }

        public SpellContainer CharacterSpells { get; private set; }

        public CharacterInformation CharacterInformation { get; private set; }

        public HealthManager HealthManager{ get; private set; }

        public void Init()
        {
            CharacterStats = new StatManager(_characterSettings.BaseStats);

            CharacterSpells = new SpellContainer(_characterSettings.BaseSpells);

            EquipmentController = new EquipmentController(this);

            HealthManager = new HealthManager(this);

            CharacterInformation = _characterSettings.CharacterInformation;

            EquipmentController.WeaponController.Equip(_characterSettings.BaseWeapon);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}