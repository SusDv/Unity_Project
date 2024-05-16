using System;
using CharacterModule.Animation;
using CharacterModule.Data.Info;
using CharacterModule.Equipment;
using CharacterModule.Settings;
using CharacterModule.Spells.Core;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.Managers.SingleStat;
using UnityEngine;

namespace CharacterModule.CharacterType.Base
{
    public class Character : MonoBehaviour, IDisposable
    {
        [SerializeField] 
        private CharacterSettings _characterSettings;
        
        public StatManager CharacterStats { get; private set; }

        public WeaponController WeaponController { get; private set; }

        public SpellContainer CharacterSpells { get; private set; }

        public CharacterInformation CharacterInformation { get; private set; }

        public HealthManager HealthManager{ get; private set; }

        [field: SerializeField] 
        public AnimationManager AnimationManager { get; private set; }

        private void Awake()
        {
            CharacterStats = new StatManager(_characterSettings.BaseStats);

            CharacterSpells = new SpellContainer(_characterSettings.BaseSpells);

            WeaponController = new WeaponController(this);

            HealthManager = new HealthManager(this);

            CharacterInformation = _characterSettings.CharacterInformation;

            WeaponController.Equip(_characterSettings.BaseWeapon);
            
            
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}