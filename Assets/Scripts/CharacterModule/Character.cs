using System;
using CharacterModule.Data.Info;
using CharacterModule.Settings;
using CharacterModule.Spells.Core;
using CharacterModule.Stats.Managers;
using UnityEngine;

namespace CharacterModule
{
    public class Character : MonoBehaviour, IDisposable
    {
        [SerializeField] private CharacterSettings _characterSettings;
        
        public StatManager CharacterStats { get; private set; }

        public CharacterWeapon CharacterWeapon { get; private set; }

        public SpellContainer CharacterSpells { get; private set; }

        public CharacterInformation CharacterInformation { get; private set; }

        public HealthManager HealthManager{ get; private set; }

        [field: SerializeField]
        public Animator CharacterAnimator { get; private set; }
        
        private void Awake()
        {
            CharacterStats = new StatManager(_characterSettings.BaseStats);

            CharacterSpells = new SpellContainer(_characterSettings.BaseSpells);

            CharacterWeapon = new CharacterWeapon(this);

            HealthManager = new HealthManager(this);

            CharacterInformation = _characterSettings.CharacterInformation;

            CharacterWeapon.EquipWeapon(_characterSettings.BaseWeapon);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}