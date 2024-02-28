using CharacterModule.Data.Info;
using CharacterModule.Settings;
using CharacterModule.Spells.Core;
using CharacterModule.Stats.Base;
using CharacterModule.Weapon;
using UnityEngine;

namespace CharacterModule
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterSettings _characterSettings;

        private CharacterWeapon _characterWeapon;

        private StatManager _characterStatManager;

        private SpellContainer _characterSpellContainer;

        private CharacterInformation _characterInformation;

        private void Awake()
        {
            _characterStatManager = new StatManager(_characterSettings.BaseStats);

            _characterSpellContainer = new SpellContainer(_characterSettings.BaseSpells);

            _characterWeapon = new CharacterWeapon(this);

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
    }
}