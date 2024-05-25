using System;
using CharacterModule.Animation;
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
        private BaseSettings _baseSettings;
        
        [field: SerializeField] 
        public AnimationManager AnimationManager { get; private set; }
        
        public StatManager Stats { get; private set; }

        public EquipmentController EquipmentController { get; private set; }

        public SpellContainer SpellContainer { get; private set; }

        public BaseInformation BaseInformation { get; private set; }

        public HealthManager HealthManager{  get; private set; }

        public void Init()
        {
            Stats = new StatManager(_baseSettings.BaseStats);

            SpellContainer = new SpellContainer(_baseSettings.BaseSpells);

            EquipmentController = new EquipmentController(this);

            HealthManager = new HealthManager(this);

            BaseInformation = _baseSettings.BaseInformation;

            EquipmentController.WeaponController.Equip(_baseSettings.BaseWeapon);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}