using System;
using CharacterModule.Data.Info;
using CharacterModule.Settings;
using CharacterModule.Spells.Core;
using CharacterModule.Stats.Managers;
using CharacterModule.Weapon;
using UnityEngine;

namespace CharacterModule
{
    public class Character : MonoBehaviour, IDisposable
    {
        [SerializeField] private CharacterSettings _characterSettings;

        private CharacterWeapon _characterWeapon;

        private StatManager _characterStatManager;

        private HealthManager _healthManager;

        private SpellContainer _characterSpellContainer;

        private CharacterInformation _characterInformation;

        [SerializeField] private Animator _characterAnimator;

        private void Awake()
        {
            _characterStatManager = new StatManager(_characterSettings.BaseStats);

            _characterSpellContainer = new SpellContainer(_characterSettings.BaseSpells);

            _characterWeapon = new CharacterWeapon(this);

            _healthManager = new HealthManager(this);

            _characterInformation = _characterSettings.CharacterInformation;

            _characterWeapon.EquipWeapon(_characterSettings.BaseWeapon);
        }

        public StatManager GetCharacterStats() 
        {
            return _characterStatManager;
        }

        public CharacterWeapon GetCharacterWeapon() 
        {
            return _characterWeapon;
        }

        public SpellContainer GetCharacterSpells() 
        {
            return _characterSpellContainer;
        }

        public CharacterInformation GetCharacterInformation()
        {
            return _characterInformation;
        }

        public HealthManager GetHealthManager()
        {
            return _healthManager;
        }

        public Animator GetCharacterAnimator()
        {
            return _characterAnimator;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}